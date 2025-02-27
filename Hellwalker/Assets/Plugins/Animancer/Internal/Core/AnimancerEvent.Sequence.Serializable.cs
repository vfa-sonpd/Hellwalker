// Animancer // Copyright 2020 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

//#define ANIMANCER_ULT_EVENTS

// If you edit this file to change the callback type to something other than UltEvents, you will need to change this
// alias as well as the HasPersistentCalls method below.
#if ANIMANCER_ULT_EVENTS
using SerializableCallback = UltEvents.UltEvent;
#else
using SerializableCallback = UnityEngine.Events.UnityEvent;
#endif

using UnityEngine;
using System;

namespace Animancer
{
    partial struct AnimancerEvent
    {
        partial class Sequence
        {
            /// <summary>
            /// An <see cref="AnimancerEvent.Sequence"/> that can be serialized and uses
            /// <see cref="SerializableCallback"/>s to define the <see cref="callback"/>s.
            /// </summary>
            /// <remarks>
            /// If you have Animancer Pro you can replace <see cref="SerializableCallback"/>s with
            /// <see href="https://kybernetik.com.au/ultevents">UltEvents</see> using the following procedure:
            /// <list type="number">
            /// <item>Select the <c>Assets/Plugins/Animancer/Animancer.asmdef</c> and add a Reference to the
            /// <c>UltEvents</c> Assembly Definition.</item>
            /// <item>Go into the Player Settings of your project and add <c>ANIMANCER_ULT_EVENTS</c> as a Scripting
            /// Define Symbol. Or you can simply edit this script to change the event type (it is located at
            /// <c>Assets/Plugins/Animancer/Internal/Core/AnimancerEvent.Sequence.Serializable.cs</c> by default.</item>
            /// </list>
            /// </remarks>
            [Serializable]
            public sealed class Serializable
#if UNITY_EDITOR
                : ISerializationCallbackReceiver
#endif
            {
                /************************************************************************************************************************/

                /// <summary>The serialized <see cref="normalizedTime"/>s.</summary>
                [SerializeField]
                private float[] _NormalizedTimes;

                /// <summary>The name of the array field which stores the <see cref="normalizedTime"/>s.</summary>
                public const string NormalizedTimesField = "_NormalizedTimes";

                /************************************************************************************************************************/

                /// <summary>The serialized <see cref="callback"/>s.</summary>
                /// <remarks>
                /// This array only needs to be large enough to hold the last event that actually contains any calls.
                /// Any empty or missing elements will simply use the <see cref="DummyCallback"/> at runtime.
                /// </remarks>
                [SerializeField]
                [UnityEngine.Serialization.FormerlySerializedAs("_Events")]
                private SerializableCallback[] _Callbacks;

                /// <summary>The name of the array field which stores the serialized <see cref="callback"/>s.</summary>
                public const string Callbacks = "_Callbacks";

                /************************************************************************************************************************/

                private Sequence _Sequence;

                /// <summary>
                /// The runtime <see cref="AnimancerEvent.Sequence"/> compiled from this <see cref="Serializable"/>.
                /// Each call after the first will return the same value.
                /// <para></para>
                /// Unlike <see cref="GetSequenceOptional"/>, this method will create an empty
                /// <see cref="AnimancerEvent.Sequence"/> instead of returning null.
                /// </summary>
                public Sequence Sequence
                {
                    get
                    {
                        if (_Sequence == null)
                        {
                            GetSequenceOptional();

                            if (_Sequence == null)
                                _Sequence = new Sequence();
                        }
                        return _Sequence;
                    }
                    set { _Sequence = value; }
                }

                /************************************************************************************************************************/

                /// <summary>
                /// Returns the runtime <see cref="AnimancerEvent.Sequence"/> compiled from this
                /// <see cref="Serializable"/>. Each call after the first will return the same value.
                /// <para></para>
                /// This method returns null if the sequence would be empty anyway and is used by the implicit
                /// conversion from <see cref="Serializable"/> to <see cref="AnimancerEvent.Sequence"/>.
                /// </summary>
                public Sequence GetSequenceOptional()
                {
                    if (_Sequence != null ||
                        _NormalizedTimes == null)
                        return _Sequence;

                    var timeCount = _NormalizedTimes.Length;
                    if (timeCount == 0)
                        return null;

                    var callbackCount = _Callbacks.Length;

                    AnimancerEvent endEvent;
                    if (callbackCount >= timeCount--)
                    {
                        endEvent = new AnimancerEvent(_NormalizedTimes[timeCount], GetInvoker(_Callbacks[timeCount]));
                    }
                    else
                    {
                        endEvent = new AnimancerEvent(_NormalizedTimes[timeCount], null);
                    }

                    _Sequence = new Sequence(timeCount)
                    {
                        endEvent = endEvent,
                        Count = timeCount,
                    };

                    for (int i = 0; i < timeCount; i++)
                    {
                        var callback = i < callbackCount ? GetInvoker(_Callbacks[i]) : DummyCallback;
                        _Sequence._Events[i] = new AnimancerEvent(_NormalizedTimes[i], callback);
                    }

                    return _Sequence;
                }

                /// <summary>Calls <see cref="GetSequenceOptional"/>.</summary>
                public static implicit operator Sequence(Serializable serializable)
                {
                    return serializable != null ? serializable.GetSequenceOptional() : null;
                }

                /************************************************************************************************************************/

                /// <summary>
                /// A delegate that does nothing which is used whenever the <see cref="SerializableCallback"/> is not
                /// defined for a particular event or it is empty.
                /// </summary>
                public static readonly Action DummyCallback = () => { };

                /// <summary>
                /// If the `callback` has any persistent calls, this method returns a delegate to call its
                /// <see cref="SerializableCallback.Invoke"/> method. Otherwise it returns the
                /// <see cref="DummyCallback"/>.
                /// </summary>
                public static Action GetInvoker(SerializableCallback callback)
                {
                    return HasPersistentCalls(callback) ? callback.Invoke : DummyCallback;
                }

                /************************************************************************************************************************/

                /// <summary>
                /// Determines if the `callback` contains any method calls that will be serialized (otherwise the
                /// <see cref="DummyCallback"/> can be used instead of creating a new delegate to invoke the empty
                /// `callback`).
                /// </summary>
                public static bool HasPersistentCalls(SerializableCallback callback)
                {
                    if (callback == null)
                        return false;

                    // UnityEvents do not allow us to check if any dynamic calls are present.
                    // But we are not giving runtime access to the events so it does not really matter.
                    // UltEvents does allow it (via the HasCalls property), but we might as well be consistent.

#if ANIMANCER_ULT_EVENTS
                    var calls = callback.PersistentCallsList;
                    return calls != null && calls.Count > 0;
#else
                    return callback.GetPersistentEventCount() > 0;
#endif
                }

                /// <summary>
                /// Determines if the `callback` contains any method calls that will be serialized (otherwise the
                /// <see cref="DummyCallback"/> can be used instead of creating a new delegate to invoke the empty
                /// `callback`).
                /// <para></para>
                /// This method casts the `callback` to <see cref="SerializableCallback"/> so the caller does not need
                /// to know what type is actually being used.
                /// </summary>
                public static bool HasPersistentCalls(object callback)
                {
                    return HasPersistentCalls((SerializableCallback)callback);
                }

                /************************************************************************************************************************/

                /// <summary>
                /// Returns the <see cref="normalizedTime"/> of the <see cref="endEvent"/>.
                /// <para></para>
                /// If the value is not set, the value is determined by <see cref="GetDefaultNormalizedEndTime"/>.
                /// </summary>
                public float GetNormalizedEndTime(float speed = 0)
                {
                    if (_NormalizedTimes == null || _NormalizedTimes.Length == 0)
                        return GetDefaultNormalizedEndTime(speed);
                    else
                        return _NormalizedTimes[_NormalizedTimes.Length - 1];
                }

                /************************************************************************************************************************/

                /// <summary>Gets the internal details of the specified `serializable`.</summary>
                public static void GetDetails(Serializable serializable, out int timeCount, out int callbackCount)
                {
                    timeCount = serializable._NormalizedTimes != null ? serializable._NormalizedTimes.Length : 0;
                    callbackCount = serializable._Callbacks != null ? serializable._Callbacks.Length : 0;
                }

                /************************************************************************************************************************/
#if UNITY_EDITOR
                /************************************************************************************************************************/

                /// <summary>[Editor-Only] Does nothing.</summary>
                void ISerializationCallbackReceiver.OnAfterDeserialize() { }

                /// <summary>[Editor-Only] Ensures that the events are sorted by time (excluding the end event).</summary>
                void ISerializationCallbackReceiver.OnBeforeSerialize()
                {
                    if (_NormalizedTimes == null ||
                        _NormalizedTimes.Length <= 2)
                        return;

                    var context = Editor.EventSequenceDrawer.Context.Instance;
                    var selectedEvent = context.Property != null ? context.SelectedEvent : -1;

                    var timeCount = _NormalizedTimes.Length - 1;
                    var callbackCount = _Callbacks != null ? _Callbacks.Length : 0;

                    var previousTime = _NormalizedTimes[0];

                    // Bubble Sort based on the normalized times.
                    var modifiedCallbacks = false;
                    for (int i = 1; i < timeCount; i++)
                    {
                        var time = _NormalizedTimes[i];
                        if (time >= previousTime)
                        {
                            previousTime = time;
                            continue;
                        }

                        _NormalizedTimes.Swap(i, i - 1);

                        if (i == callbackCount)
                        {
                            Array.Resize(ref _Callbacks, ++callbackCount);
                        }

                        if (i < callbackCount)
                        {
                            _Callbacks.Swap(i, i - 1);
                            modifiedCallbacks = true;
                        }

                        if (selectedEvent == i)
                            selectedEvent = i - 1;
                        else if (selectedEvent == i - 1)
                            selectedEvent = i;

                        if (i == 1)
                        {
                            i = 0;
                            previousTime = float.NegativeInfinity;
                        }
                        else
                        {
                            i -= 2;
                            previousTime = _NormalizedTimes[i];
                        }
                    }

                    // If the current animation is looping, clamp all times within the 0-1 range.
                    var transitionContext = Editor.TransitionDrawer.Context;
                    if (transitionContext != null &&
                        transitionContext.Transition != null &&
                        transitionContext.Transition.IsLooping)
                    {
                        for (int i = _NormalizedTimes.Length - 1; i >= 0; i--)
                        {
                            var time = _NormalizedTimes[i];
                            if (time < 0)
                                _NormalizedTimes[i] = 0;
                            else if (time > AlmostOne)
                                _NormalizedTimes[i] = AlmostOne;
                        }
                    }

                    // If the selected event was moved adjust the selection.
                    if (context.Property != null && context.SelectedEvent != selectedEvent)
                    {
                        context.SelectedEvent = selectedEvent;
                        Editor.TransitionPreviewWindow.SetPreviewNormalizedTime(_NormalizedTimes[selectedEvent]);
                    }

                    // Remove any empty callbacks from the end of the array to reduce the serialized data size.
                    if (modifiedCallbacks)
                    {
                        while (callbackCount >= 1)
                        {
                            var callback = _Callbacks[callbackCount - 1];
                            if (callback != null && HasPersistentCalls(callback))
                                break;
                            else
                                callbackCount--;
                        }

                        Array.Resize(ref _Callbacks, callbackCount);
                    }
                }

                /************************************************************************************************************************/
#endif
                /************************************************************************************************************************/
            }
        }
    }

    /************************************************************************************************************************/
#if UNITY_EDITOR
    /************************************************************************************************************************/

    namespace Editor
    {
        /// <summary>[Editor-Only] [Internal]
        /// A serializable container which holds a <see cref="SerializableCallback"/> in a field named "_Callback".
        /// </summary>
        /// <remarks>
        /// <see cref="DummySerializableCallback"/> needs to be in a file with the same name as it (otherwise it can't
        /// draw the callback properly) and this class needs to be in the same file as
        /// <see cref="AnimancerEvent.Sequence.Serializable"/> to use the <see cref="SerializableCallback"/> alias.
        /// </remarks>
        [Serializable]
        internal sealed class SerializableCallbackHolder
        {
#pragma warning disable CS0169 // Field is never used.
            [SerializeField]
            private SerializableCallback _Callback;
        }
    }

    /************************************************************************************************************************/
#endif
    /************************************************************************************************************************/
}

