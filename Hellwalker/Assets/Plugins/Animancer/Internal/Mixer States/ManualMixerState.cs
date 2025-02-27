// Animancer // Copyright 2020 Kybernetik //

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;
#endif

namespace Animancer
{
    /// <summary>[Pro-Only]
    /// An <see cref="AnimancerState"/> which blends multiple child states. Unlike other mixers, this class does not
    /// perform any automatic weight calculations, it simple allows you to control the weight of all states manually.
    /// <para></para>
    /// This mixer type is similar to the Direct Blend Type in Mecanim Blend Trees.
    /// </summary>
    public class ManualMixerState : MixerState
    {
        /************************************************************************************************************************/
        #region Properties
        /************************************************************************************************************************/

        /// <summary>The states managed by this mixer.</summary>
        private AnimancerState[] _States;

        /// <summary>Returns the <see cref="_States"/> array.</summary>
        public override IList<AnimancerState> ChildStates { get { return _States; } }

        /************************************************************************************************************************/

        /// <summary>The number of states using this mixer as their <see cref="AnimancerState.Parent"/>.</summary>
        public override int ChildCount { get { return _States.Length; } }

        /// <summary>Returns the state connected to the specified `index` as a child of this mixer.</summary>
        public override AnimancerState GetChild(int index)
        {
            return _States[index];
        }

        /// <summary>Assigns the `state` as a child of this mixer.</summary>
        public void SetChild(int index, AnimancerState state)
        {
            state.SetParent(this, index);
        }

        /************************************************************************************************************************/

        /// <summary>
        /// The weighted average <see cref="AnimancerState.Time"/> of each child state according to their
        /// <see cref="AnimancerNode.Weight"/>.
        /// </summary>
        protected override float RawTime
        {
            get
            {
                var totalWeight = 0f;
                var normalizedTime = 0f;
                var length = 0f;

                for (int i = _States.Length - 1; i >= 0; i--)
                {
                    var state = _States[i];
                    if (state != null)
                    {
                        var weight = state.Weight;
                        totalWeight += weight;
                        normalizedTime += state.NormalizedTime * weight;
                        length += state.Length * weight;
                    }
                }

                if (totalWeight == 0)
                    return 0;

                totalWeight = 1f / totalWeight;
                return normalizedTime * totalWeight * length * totalWeight;
            }
            set
            {
                var childCount = _States.Length;

                if (value == 0)
                    goto ZeroTime;

                var length = Length;
                if (length == 0)
                    goto ZeroTime;

                value /= length;// Normalize.

                while (--childCount >= 0)
                {
                    var state = _States[childCount];
                    if (state != null)
                        state.NormalizedTime = value;
                }

                return;

                // If the value is 0, we can set the child times slightly more efficiently.
                ZeroTime:
                while (--childCount >= 0)
                {
                    var state = _States[childCount];
                    if (state != null)
                        state.Time = 0;
                }
            }
        }

        /************************************************************************************************************************/

        /// <summary>
        /// The weighted average <see cref="AnimancerState.Length"/> of each child state according to their
        /// <see cref="AnimancerNode.Weight"/>.
        /// </summary>
        public override float Length
        {
            get
            {
                var length = 0f;

                var childWeight = CalculateTotalChildWeight();
                if (childWeight == 0)
                    return 0;

                childWeight = 1f / childWeight;

                for (int i = _States.Length - 1; i >= 0; i--)
                {
                    var state = _States[i];
                    if (state != null)
                        length += state.Length * state.Weight * childWeight;
                }

                return length;
            }
        }

        /************************************************************************************************************************/
        #endregion
        /************************************************************************************************************************/
        #region Initialisation
        /************************************************************************************************************************/

        /// <summary>
        /// Initialises this mixer with the specified number of ports which can be filled individually by <see cref="CreateState"/>.
        /// </summary>
        public virtual void Initialise(int childCount)
        {
#if UNITY_ASSERTIONS
            if (childCount <= 1)
                Debug.LogWarning(GetType() + " is being initialised with capacity <= 1. The purpose of a mixer is to mix multiple clips.");
#endif

            if (_States != null)
            {
                for (int i = _States.Length - 1; i >= 0; i--)
                {
                    var state = _States[i];
                    if (state == null)
                        continue;

                    state.ClearParent();
                }
            }

            DestroyPlayable();
            _States = new AnimancerState[childCount];
        }

        /************************************************************************************************************************/

        /// <summary>
        /// Initialises this mixer with one state per clip.
        /// </summary>
        public void Initialise(params AnimationClip[] clips)
        {
            var count = clips.Length;
            Initialise(count);

            for (int i = 0; i < count; i++)
            {
                var clip = clips[i];
                if (clip != null)
                    CreateState(i, clip);
            }
        }

        /************************************************************************************************************************/

        /// <summary>Indicates whether the child states of this mixer have been initialised.</summary>
        public bool IsInitialised
        {
            get { return _States != null; }
        }

        /************************************************************************************************************************/

        /// <summary>
        /// Creates and returns a new <see cref="ClipState"/> to play the `clip` with this mixer as its parent.
        /// </summary>
        public override ClipState CreateState(int index, AnimationClip clip)
        {
            var state = new ClipState(clip)
            {
                IsPlaying = IsPlaying
            };
            SetChild(index, state);
            return state;
        }

        /************************************************************************************************************************/

        /// <summary>
        /// Creates and returns a new <see cref="ClipState"/> to play the `clip` with this
        /// <see cref="MixerState"/> as its parent.
        /// </summary>
        public ClipState GetOrCreateState(int index, AnimationClip clip)
        {
            var oldState = _States[index];
            if (oldState != null)
            {
                // If the old state has the specified `clip`, return it.
                if (oldState.Clip == clip)
                {
                    return oldState as ClipState;
                }
                else// Otherwise destroy and replace it.
                {
                    oldState.Destroy();
                }
            }

            return CreateState(index, clip);
        }

        /************************************************************************************************************************/

        /// <summary>Connects the `state` to this mixer at its <see cref="AnimancerNode.Index"/>.</summary>
        protected internal override void OnAddChild(AnimancerState state)
        {
            OnAddChild(_States, state);
        }

        /// <summary>Disconnects the `state` from this mixer at its <see cref="AnimancerNode.Index"/>.</summary>
        protected internal override void OnRemoveChild(AnimancerState state)
        {
            Validate.RemoveChild(state, _States);
            _States[state.Index] = null;
            state.DisconnectFromGraph();
        }

        /************************************************************************************************************************/

        /// <summary>
        /// Destroys the <see cref="Playable"/> of this mixer and its <see cref="_States"/>.
        /// </summary>
        public override void Destroy()
        {
            DestroyChildren();
            base.Destroy();
        }

        /************************************************************************************************************************/
        #endregion
        /************************************************************************************************************************/
        #region Child States
        /************************************************************************************************************************/

        /// <summary>
        /// Calculates the sum of the <see cref="AnimancerNode.Weight"/> of all child states.
        /// </summary>
        public float CalculateTotalChildWeight()
        {
            if (_States == null)
                return 0;

            var total = 0f;

            for (int i = _States.Length - 1; i >= 0; i--)
            {
                var state = _States[i];
                if (state != null)
                    total += state.Weight;
            }

            return total;
        }

        /************************************************************************************************************************/

        /// <summary>
        /// Destroys all <see cref="_States"/> connected to this mixer. This operation cannot be undone.
        /// </summary>
        public void DestroyChildren()
        {
            if (_States == null)
                return;

            for (int i = _States.Length - 1; i >= 0; i--)
            {
                var state = _States[i];
                if (state != null)
                    state.Destroy();
            }

            _States = null;
        }

        /************************************************************************************************************************/

        /// <summary>
        /// Does nothing. Manual mixers do not automatically recalculate their weights.
        /// </summary>
        public override void RecalculateWeights() { }

        /************************************************************************************************************************/

        /// <summary>
        /// Sets the weight of all states after the `previousIndex` to 0.
        /// </summary>
        protected void DisableRemainingStates(int previousIndex)
        {
            var childCount = _States.Length;
            while (++previousIndex < childCount)
            {
                var state = _States[previousIndex];
                if (state == null)
                    continue;

                state.Weight = 0;
            }
        }

        /************************************************************************************************************************/

        /// <summary>
        /// Returns the state at the specified `index` if it is not null, otherwise increments the index and checks
        /// again. Returns null if no state is found by the end of the <see cref="_States"/> array.
        /// </summary>
        protected AnimancerState GetNextState(ref int index)
        {
            while (index < _States.Length)
            {
                var state = _States[index];
                if (state != null)
                    return state;

                index++;
            }

            return null;
        }

        /************************************************************************************************************************/

        /// <summary>
        /// Divides the weight of all states by the `totalWeight` so that they all add up to 1.
        /// </summary>
        protected void NormalizeWeights(float totalWeight)
        {
            if (totalWeight == 1)
                return;

            totalWeight = 1f / totalWeight;

            for (int i = _States.Length - 1; i >= 0; i--)
            {
                var state = _States[i];
                if (state == null)
                    continue;

                state.Weight *= totalWeight;
            }
        }

        /************************************************************************************************************************/

        /// <summary>Gets a user-friendly key to identify the `state` in the Inspector.</summary>
        public override string GetDisplayKey(AnimancerState state)
        {
            return string.Concat("[", state.Index.ToString(), "]");
        }

        /************************************************************************************************************************/
        #endregion
        /************************************************************************************************************************/
        #region Transition
        /************************************************************************************************************************/

        /// <summary>
        /// Base class for serializable <see cref="ITransition"/>s which can create a particular type of
        /// <see cref="ManualMixerState"/> when passed into <see cref="AnimancerPlayable.Play(ITransition)"/>.
        /// </summary>
        /// <remarks>
        /// Unfortunately the tool used to generate this documentation does not currently support nested types with
        /// identical names, so only one <c>Transition</c> class will actually have a documentation page.
        /// <para></para>
        /// Even though it has the <see cref="SerializableAttribute"/>, this class won't actually get serialized
        /// by Unity because it's generic and abstract. Each child class still needs to include the attribute.
        /// </remarks>
        [Serializable]
        public abstract new class Transition<TMixer> : AnimancerState.Transition<TMixer>, IAnimationClipCollection
            where TMixer : ManualMixerState
        {
            /************************************************************************************************************************/

            [SerializeField, Tooltip(Strings.ProOnlyTag +
                "How fast the mixer plays (1x = normal speed, 2x = double speed)")]
            private float _Speed = 1;

            /// <summary>[<see cref="SerializeField"/>]
            /// Determines how fast the mixer plays (1x = normal speed, 2x = double speed).
            /// </summary>
            public override float Speed
            {
                get { return _Speed; }
                set { _Speed = value; }
            }

            /************************************************************************************************************************/

            [SerializeField, HideInInspector]
            private AnimationClip[] _Clips;

            /// <summary>[<see cref="SerializeField"/>]
            /// The <see cref="ClipState.Clip"/> to use for each state in the mixer.
            /// </summary>
            public AnimationClip[] Clips
            {
                get { return _Clips; }
                set { _Clips = value; }
            }

            [SerializeField, HideInInspector]
            private float[] _Speeds;

            /// <summary>[<see cref="SerializeField"/>]
            /// The <see cref="AnimancerNode.Speed"/> to use for each state in the mixer.
            /// <para></para>
            /// If the size of this array doesn't match the <see cref="Clips"/>, it will be ignored.
            /// </summary>
            public float[] Speeds
            {
                get { return _Speeds; }
                set { _Speeds = value; }
            }

            /************************************************************************************************************************/

            [SerializeField, HideInInspector]
            private bool[] _SynchroniseChildren;

            /// <summary>[<see cref="SerializeField"/>]
            /// The <see cref="MixerState.SynchroniseChildren"/> flags for each state in the mixer.
            /// <para></para>
            /// The array can be null or empty. Any elements not in the array will be treated as true.
            /// </summary>
            public bool[] SynchroniseChildren
            {
                get { return _SynchroniseChildren; }
                set { _SynchroniseChildren = value; }
            }

            /************************************************************************************************************************/

            /// <summary>[<see cref="ITransitionDetailed"/>]
            /// Returns true is any of the <see cref="Clips"/> are looping.
            /// </summary>
            public override bool IsLooping
            {
                get
                {
                    for (int i = _Clips.Length - 1; i >= 0; i--)
                    {
                        var clip = _Clips[i];
                        if (clip == null)
                            continue;

                        if (clip.isLooping)
                            return true;
                    }

                    return false;
                }
            }

            /// <summary>[<see cref="ITransitionDetailed"/>]
            /// The maximum amount of time the animation is expected to take (in seconds).
            /// </summary>
            public override float MaximumDuration
            {
                get
                {
                    if (_Clips == null)
                        return 0;

                    var duration = 0f;
                    var hasSpeeds = _Speeds != null && _Speeds.Length == _Clips.Length;

                    for (int i = _Clips.Length - 1; i >= 0; i--)
                    {
                        var clip = _Clips[i];
                        if (clip == null)
                            continue;

                        var length = clip.length;

                        if (hasSpeeds)
                            length *= _Speeds[i];

                        if (duration < length)
                            duration = length;
                    }

                    return duration;
                }
            }

            /************************************************************************************************************************/

            /// <summary>
            /// Initialises the <see cref="AnimancerState.Transition{TState}.State"/> immediately after it is created.
            /// </summary>
            public virtual void InitialiseState()
            {
                State.Initialise(_Clips);

                if (_Speeds != null)
                {
#if UNITY_ASSERTIONS
                    if (_Speeds.Length != 0 && _Speeds.Length != _Clips.Length)
                        Debug.LogError(string.Concat(
                            "The number of serialized speeds (", _Speeds.Length,
                            ") does not match the number of clips (", _Clips.Length, ")."));
#endif

                    for (int i = _Speeds.Length - 1; i >= 0; i--)
                    {
                        State._States[i].Speed = _Speeds[i];
                    }
                }

                State.SynchroniseChildren = _SynchroniseChildren;
            }

            /************************************************************************************************************************/

            /// <summary>
            /// Called by <see cref="AnimancerPlayable.Play(ITransition)"/> to apply the <see cref="Speed"/>.
            /// </summary>
            public override void Apply(AnimancerState state)
            {
                base.Apply(state);

                if (!float.IsNaN(_Speed))
                    state.Speed = _Speed;
            }

            /************************************************************************************************************************/

            /// <summary>Adds the <see cref="Clips"/> to the collection.</summary>
            void IAnimationClipCollection.GatherAnimationClips(ICollection<AnimationClip> clips)
            {
                clips.Gather(_Clips);
            }

            /************************************************************************************************************************/
        }

        /************************************************************************************************************************/

        /// <summary>
        /// A serializable <see cref="ITransition"/> which can create a <see cref="ManualMixerState"/> when
        /// passed into <see cref="AnimancerPlayable.Play(ITransition)"/>.
        /// </summary>
        /// <remarks>
        /// Unfortunately the tool used to generate this documentation does not currently support nested types with
        /// identical names, so only one <c>Transition</c> class will actually have a documentation page.
        /// </remarks>
        [Serializable]
        public class Transition : Transition<ManualMixerState>
        {
            /************************************************************************************************************************/

            /// <summary>
            /// Creates and returns a new <see cref="ManualMixerState"/> connected to the `layer`.
            /// <para></para>
            /// This method also assigns it as the <see cref="AnimancerState.Transition{TState}.State"/>.
            /// </summary>
            public override ManualMixerState CreateState()
            {
                State = new ManualMixerState();
                InitialiseState();
                return State;
            }

            /************************************************************************************************************************/
            #region Drawer
#if UNITY_EDITOR
            /************************************************************************************************************************/

            /// <summary>[Editor-Only] Draws the Inspector GUI for a <see cref="Transition"/>.</summary>
            [CustomPropertyDrawer(typeof(Transition), true)]
            public class Drawer : Editor.TransitionDrawer
            {
                /************************************************************************************************************************/

                /// <summary>
                /// The property this drawer is currently drawing.
                /// <para></para>
                /// Normally each property has its own drawer, but arrays share a single drawer for all elements.
                /// </summary>
                public static SerializedProperty CurrentProperty { get; private set; }

                /// <summary>The <see cref="Transition{TState}.Clips"/> field.</summary>
                public static SerializedProperty CurrentClips { get; private set; }

                /// <summary>The <see cref="Transition{TState}.Speeds"/> field.</summary>
                public static SerializedProperty CurrentSpeeds { get; private set; }

                /// <summary>The <see cref="Transition{TState}.SynchroniseChildren"/> field.</summary>
                public static SerializedProperty CurrentSynchroniseChildren { get; private set; }

                private readonly Dictionary<string, ReorderableList>
                    PropertyPathToStates = new Dictionary<string, ReorderableList>();

                /************************************************************************************************************************/

                /// <summary>
                /// Gather the details of the `property`.
                /// <para></para>
                /// This method gets called by every <see cref="GetPropertyHeight"/> and <see cref="OnGUI"/> call since
                /// Unity uses the same <see cref="PropertyDrawer"/> instance for each element in a collection, so it
                /// needs to gather the details associated with the current property.
                /// </summary>
                protected virtual ReorderableList GatherDetails(SerializedProperty property)
                {
                    InitialiseMode(property);
                    GatherSubProperties(property);

                    var propertyPath = property.propertyPath;

                    ReorderableList states;
                    if (!PropertyPathToStates.TryGetValue(propertyPath, out states))
                    {
                        states = new ReorderableList(CurrentClips.serializedObject, CurrentClips)
                        {
                            drawHeaderCallback = DoStateListHeaderGUI,
                            elementHeightCallback = GetElementHeight,
                            drawElementCallback = DoElementGUI,
                            onAddCallback = OnAddElement,
                            onRemoveCallback = OnRemoveElement,
#if UNITY_2018_1_OR_NEWER
                            onReorderCallbackWithDetails = OnReorderList,
#else
                            onReorderCallback = OnReorderList,
                            onSelectCallback = OnListSelectionChanged,
#endif
                        };

                        PropertyPathToStates.Add(propertyPath, states);
                    }

                    states.serializedProperty = CurrentClips;

                    return states;
                }

                /************************************************************************************************************************/

                /// <summary>
                /// Called every time a `property` is drawn to find the relevant child properties and store them to be
                /// used in <see cref="GetPropertyHeight"/> and <see cref="OnGUI"/>.
                /// </summary>
                protected virtual void GatherSubProperties(SerializedProperty property)
                {
                    GatherSubPropertiesStatic(property);
                }

                /// <summary>
                /// Called every time a `property` is drawn to find the relevant child properties and store them to be
                /// used in <see cref="GetPropertyHeight"/> and <see cref="OnGUI"/>.
                /// </summary>
                public static void GatherSubPropertiesStatic(SerializedProperty property)
                {
                    CurrentProperty = property;
                    CurrentClips = property.FindPropertyRelative("_Clips");
                    CurrentSpeeds = property.FindPropertyRelative("_Speeds");
                    CurrentSynchroniseChildren = property.FindPropertyRelative("_SynchroniseChildren");

                    if (CurrentSpeeds.arraySize != 0)
                        CurrentSpeeds.arraySize = CurrentClips.arraySize;
                }

                /************************************************************************************************************************/

                /// <summary>
                /// Adds a menu item that will call <see cref="GatherSubPropertiesStatic"/> then run the specified
                /// `function`.
                /// </summary>
                protected static void AddPropertyModifierFunction(GenericMenu menu, string label,
                    Editor.MenuFunctionState state, Action<SerializedProperty> function)
                {
                    Editor.Serialization.AddPropertyModifierFunction(menu, CurrentProperty, label, state, (property) =>
                    {
                        GatherSubPropertiesStatic(property);
                        function(property);
                    });
                }

                /// <summary>
                /// Adds a menu item that will call <see cref="GatherSubPropertiesStatic"/> then run the specified
                /// `function`.
                /// </summary>
                protected static void AddPropertyModifierFunction(GenericMenu menu, string label,
                    Action<SerializedProperty> function)
                {
                    Editor.Serialization.AddPropertyModifierFunction(menu, CurrentProperty, label, (property) =>
                    {
                        GatherSubPropertiesStatic(property);
                        function(property);
                    });
                }

                /************************************************************************************************************************/

                /// <summary>
                /// Calculates the number of vertical pixels the `property` will occupy when it is drawn.
                /// </summary>
                public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
                {
                    var height = EditorGUI.GetPropertyHeight(property, label);

                    if (property.isExpanded)
                    {
                        var states = GatherDetails(property);
                        height += Editor.AnimancerGUI.StandardSpacing + states.GetHeight();
                    }

                    return height;
                }

                /************************************************************************************************************************/

                /// <summary>
                /// Draws the root `property` GUI and calls
                /// <see cref="Editor.TransitionDrawer.DoPropertyGUI"/> for each of its children.
                /// </summary>
                public override void OnGUI(Rect area, SerializedProperty property, GUIContent label)
                {
                    var originalProperty = property.Copy();

                    base.OnGUI(area, property, label);

                    if (!originalProperty.isExpanded)
                        return;

                    using (TransitionContext.Get(this, property))
                    {
                        var states = GatherDetails(originalProperty);

                        var indentLevel = EditorGUI.indentLevel;

                        area.yMin = area.yMax - states.GetHeight();

                        EditorGUI.indentLevel++;
                        area = EditorGUI.IndentedRect(area);

                        EditorGUI.indentLevel = 0;
                        states.DoList(area);

                        EditorGUI.indentLevel = indentLevel;

                        TryCollapseArrays();
                    }
                }

                /************************************************************************************************************************/

                private static float _SpeedLabelWidth;
                private static float _SyncLabelWidth;

                /// <summary>Splits the specified `area` into separate sections.</summary>
                protected static void SplitListRect(Rect area, bool isHeader, out Rect animation, out Rect speed, out Rect sync)
                {
                    if (_SpeedLabelWidth == 0)
                        _SpeedLabelWidth = Editor.AnimancerGUI.CalculateWidth(EditorStyles.popup, "Speed");

                    if (_SyncLabelWidth == 0)
                        _SyncLabelWidth = Editor.AnimancerGUI.CalculateWidth(EditorStyles.popup, "Sync");

                    var spacing = Editor.AnimancerGUI.StandardSpacing;

                    var syncWidth = isHeader ?
                        _SyncLabelWidth :
                        Editor.AnimancerGUI.ToggleWidth - spacing;

                    var speedWidth = _SpeedLabelWidth + _SyncLabelWidth - syncWidth;

                    area.width += spacing;
                    sync = Editor.AnimancerGUI.StealFromRight(ref area, syncWidth, spacing);
                    speed = Editor.AnimancerGUI.StealFromRight(ref area, speedWidth, spacing);
                    animation = area;
                }

                /************************************************************************************************************************/
                #region Headers
                /************************************************************************************************************************/

                /// <summary>Draws the headdings of the state list.</summary>
                protected virtual void DoStateListHeaderGUI(Rect area)
                {
                    Rect animationArea, speedArea, syncArea;
                    SplitListRect(area, true, out animationArea, out speedArea, out syncArea);

                    DoAnimationHeaderGUI(animationArea);
                    DoSpeedHeaderGUI(speedArea);
                    DoSyncHeaderGUI(syncArea);
                }

                /************************************************************************************************************************/

                /// <summary>Draws an "Animation" header.</summary>
                protected static void DoAnimationHeaderGUI(Rect area)
                {
                    var content = Editor.AnimancerGUI.TempContent("Animation",
                        "The animations that will be used for each child state");
                    DoHeaderDropdownGUI(area, CurrentClips, content, null);
                }

                /************************************************************************************************************************/
                #region Speeds
                /************************************************************************************************************************/

                /// <summary>Draws a "Speed" header.</summary>
                protected static void DoSpeedHeaderGUI(Rect area)
                {
                    var content = Editor.AnimancerGUI.TempContent("Speed",
                        "Determines how fast each child state plays (Default = 1)");
                    DoHeaderDropdownGUI(area, CurrentSpeeds, content, (menu) =>
                    {
                        AddPropertyModifierFunction(menu, "Reset All to 1",
                            CurrentSpeeds.arraySize == 0 ? Editor.MenuFunctionState.Selected : Editor.MenuFunctionState.Normal,
                            (_) => CurrentSpeeds.arraySize = 0);

                        AddPropertyModifierFunction(menu, "Normalize Durations", Editor.MenuFunctionState.Normal, NormalizeDurations);
                    });
                }

                /************************************************************************************************************************/

                /// <summary>
                /// Recalculates the <see cref="CurrentSpeeds"/> depending on the <see cref="AnimationClip.length"/> of
                /// their animations so that they all take the same amount of time to play fully.
                /// </summary>
                private static void NormalizeDurations(SerializedProperty property)
                {
                    var speedCount = CurrentSpeeds.arraySize;

                    var lengths = new float[CurrentClips.arraySize];
                    if (lengths.Length <= 1)
                        return;

                    int nonZeroLengths = 0;
                    float totalLength = 0;
                    float totalSpeed = 0;
                    for (int i = 0; i < lengths.Length; i++)
                    {
                        var clip = CurrentClips.GetArrayElementAtIndex(i).objectReferenceValue as AnimationClip;
                        if (clip != null && clip.length > 0)
                        {
                            nonZeroLengths++;
                            totalLength += clip.length;
                            lengths[i] = clip.length;

                            if (speedCount > 0)
                                totalSpeed += CurrentSpeeds.GetArrayElementAtIndex(i).floatValue;
                        }
                    }

                    if (nonZeroLengths == 0)
                        return;

                    var averageLength = totalLength / nonZeroLengths;
                    var averageSpeed = speedCount > 0 ? totalSpeed / nonZeroLengths : 1;

                    CurrentSpeeds.arraySize = lengths.Length;
                    InitialiseSpeeds(speedCount);

                    for (int i = 0; i < lengths.Length; i++)
                    {
                        if (lengths[i] == 0)
                            continue;

                        CurrentSpeeds.GetArrayElementAtIndex(i).floatValue = averageSpeed * lengths[i] / averageLength;
                    }

                    TryCollapseArrays();
                }

                /************************************************************************************************************************/

                /// <summary>
                /// Initialises every element in the <see cref="CurrentSpeeds"/> array from the `start` to the end of
                /// the array to contain a value of 1.
                /// </summary>
                public static void InitialiseSpeeds(int start)
                {
                    var count = CurrentSpeeds.arraySize;
                    while (start < count)
                        CurrentSpeeds.GetArrayElementAtIndex(start++).floatValue = 1;
                }

                /************************************************************************************************************************/
                #endregion
                /************************************************************************************************************************/
                #region Sync
                /************************************************************************************************************************/

                /// <summary>Draws a "Sync" header.</summary>
                protected static void DoSyncHeaderGUI(Rect area)
                {
                    var content = Editor.AnimancerGUI.TempContent("Sync",
                        "Determines which child states have their normalized times constantly synchronised");
                    DoHeaderDropdownGUI(area, CurrentSpeeds, content, (menu) =>
                    {
                        var syncCount = CurrentSynchroniseChildren.arraySize;

                        var allState = syncCount == 0 ? Editor.MenuFunctionState.Selected : Editor.MenuFunctionState.Normal;
                        AddPropertyModifierFunction(menu, "All", allState,
                            (_) => CurrentSynchroniseChildren.arraySize = 0);

                        var syncNone = syncCount == CurrentClips.arraySize;
                        if (syncNone)
                        {
                            for (int i = 0; i < syncCount; i++)
                            {
                                if (CurrentSynchroniseChildren.GetArrayElementAtIndex(i).boolValue)
                                {
                                    syncNone = false;
                                    break;
                                }
                            }
                        }
                        var noneState = syncNone ? Editor.MenuFunctionState.Selected : Editor.MenuFunctionState.Normal;
                        AddPropertyModifierFunction(menu, "None", noneState, (_) =>
                        {
                            var count = CurrentSynchroniseChildren.arraySize = CurrentClips.arraySize;
                            for (int i = 0; i < count; i++)
                                CurrentSynchroniseChildren.GetArrayElementAtIndex(i).boolValue = false;
                        });

                        AddPropertyModifierFunction(menu, "Toggle", Editor.MenuFunctionState.Normal,
                            (_) =>
                            {
                                var count = CurrentSynchroniseChildren.arraySize;
                                for (int i = 0; i < count; i++)
                                {
                                    var property = CurrentSynchroniseChildren.GetArrayElementAtIndex(i);
                                    property.boolValue = !property.boolValue;
                                }

                                var newCount = CurrentSynchroniseChildren.arraySize = CurrentClips.arraySize;
                                for (int i = count; i < newCount; i++)
                                    CurrentSynchroniseChildren.GetArrayElementAtIndex(i).boolValue = false;
                            });

                        AddPropertyModifierFunction(menu, "Non-Stationary", Editor.MenuFunctionState.Normal,
                            (_) =>
                            {
                                var count = CurrentClips.arraySize;

                                for (int i = 0; i < count; i++)
                                {
                                    var clip = CurrentClips.GetArrayElementAtIndex(i).objectReferenceValue as AnimationClip;
                                    if (clip == null)
                                        continue;

                                    if (i >= syncCount)
                                    {
                                        CurrentSynchroniseChildren.arraySize = i + 1;
                                        for (int j = syncCount; j < i; j++)
                                            CurrentSynchroniseChildren.GetArrayElementAtIndex(j).boolValue = true;
                                        syncCount = i + 1;
                                    }

                                    CurrentSynchroniseChildren.GetArrayElementAtIndex(i).boolValue =
                                        clip.averageSpeed != Vector3.zero;
                                }

                                TryCollapseSync();
                            });
                    });
                }

                /************************************************************************************************************************/

                private static void SyncNone()
                {
                    var count = CurrentSynchroniseChildren.arraySize = CurrentClips.arraySize;
                    for (int i = 0; i < count; i++)
                        CurrentSynchroniseChildren.GetArrayElementAtIndex(i).boolValue = false;
                }

                /************************************************************************************************************************/
                #endregion
                /************************************************************************************************************************/

                /// <summary>Draws the GUI for a header dropdown button.</summary>
                public static void DoHeaderDropdownGUI(Rect area, SerializedProperty property, GUIContent content,
                    Action<GenericMenu> populateMenu)
                {
                    if (property != null)
                        EditorGUI.BeginProperty(area, GUIContent.none, property);

                    if (populateMenu != null)
                    {
                        if (EditorGUI.DropdownButton(area, content, FocusType.Passive))
                        {
                            var menu = new GenericMenu();
                            populateMenu(menu);
                            menu.ShowAsContext();
                        }
                    }
                    else
                    {
                        GUI.Label(area, content);
                    }

                    if (property != null)
                        EditorGUI.EndProperty();
                }

                /************************************************************************************************************************/
                #endregion
                /************************************************************************************************************************/

                /// <summary>Calculates the height of the state at the specified `index`.</summary>
                protected virtual float GetElementHeight(int index)
                {
                    return Editor.AnimancerGUI.LineHeight;
                }

                /************************************************************************************************************************/

                /// <summary>Draws the GUI of the state at the specified `index`.</summary>
                private void DoElementGUI(Rect area, int index, bool isActive, bool isFocused)
                {
                    if (index < 0 || index > CurrentClips.arraySize)
                        return;

                    var clip = CurrentClips.GetArrayElementAtIndex(index);
                    var speed = CurrentSpeeds.arraySize > 0 ? CurrentSpeeds.GetArrayElementAtIndex(index) : null;
                    DoElementGUI(area, index, clip, speed);
                }

                /************************************************************************************************************************/

                /// <summary>Draws the GUI of the state at the specified `index`.</summary>
                protected virtual void DoElementGUI(Rect area, int index,
                    SerializedProperty clip, SerializedProperty speed)
                {
                    Rect animationArea, speedArea, syncArea;
                    SplitListRect(area, false, out animationArea, out speedArea, out syncArea);

                    DoElementGUI(animationArea, speedArea, syncArea, index, clip, speed);
                }

                /// <summary>Draws the GUI of the state at the specified `index`.</summary>
                protected void DoElementGUI(Rect animationArea, Rect speedArea, Rect syncArea, int index,
                    SerializedProperty clip, SerializedProperty speed)
                {
                    EditorGUI.PropertyField(animationArea, clip, GUIContent.none);

                    if (speed != null)
                    {
                        EditorGUI.PropertyField(speedArea, speed, GUIContent.none);
                    }
                    else
                    {
                        EditorGUI.BeginProperty(speedArea, GUIContent.none, CurrentSpeeds);

                        var value = EditorGUI.FloatField(speedArea, 1);
                        if (value != 1)
                        {
                            CurrentSpeeds.InsertArrayElementAtIndex(0);
                            CurrentSpeeds.GetArrayElementAtIndex(0).floatValue = 1;
                            CurrentSpeeds.arraySize = CurrentClips.arraySize;
                            CurrentSpeeds.GetArrayElementAtIndex(index).floatValue = value;
                        }

                        EditorGUI.EndProperty();
                    }

                    DoSyncToggleGUI(syncArea, index);
                }

                /************************************************************************************************************************/

                /// <summary>
                /// Draws a toggle to enable or disable <see cref="MixerState.SynchroniseChildren"/> for the child at
                /// the specified `index`.
                /// </summary>
                protected void DoSyncToggleGUI(Rect area, int index)
                {
                    var syncProperty = CurrentSynchroniseChildren;
                    var syncFlagCount = syncProperty.arraySize;

                    var enabled = true;

                    if (index < syncFlagCount)
                    {
                        syncProperty = syncProperty.GetArrayElementAtIndex(index);
                        enabled = syncProperty.boolValue;
                    }

                    EditorGUI.BeginChangeCheck();
                    EditorGUI.BeginProperty(area, GUIContent.none, syncProperty);

                    enabled = GUI.Toggle(area, enabled, GUIContent.none);

                    EditorGUI.EndProperty();
                    if (EditorGUI.EndChangeCheck())
                    {
                        if (index < syncFlagCount)
                        {
                            syncProperty.boolValue = enabled;
                        }
                        else
                        {
                            syncProperty.arraySize = index + 1;

                            for (int i = syncFlagCount; i < index; i++)
                            {
                                syncProperty.GetArrayElementAtIndex(i).boolValue = true;
                            }

                            syncProperty.GetArrayElementAtIndex(index).boolValue = enabled;
                        }
                    }
                }

                /************************************************************************************************************************/

                /// <summary>
                /// Called when adding a new state to the list to ensure that any other relevant arrays have new
                /// elements added as well.
                /// </summary>
                protected virtual void OnAddElement(ReorderableList list)
                {
                    var index = CurrentClips.arraySize;
                    CurrentClips.InsertArrayElementAtIndex(index);

                    if (CurrentSpeeds.arraySize > 0)
                        CurrentSpeeds.InsertArrayElementAtIndex(index);
                }

                /************************************************************************************************************************/

                /// <summary>
                /// Called when removing a state from the list to ensure that any other relevant arrays have elements
                /// removed as well.
                /// </summary>
                protected virtual void OnRemoveElement(ReorderableList list)
                {
                    var index = list.index;

                    RemoveArrayElement(CurrentClips, index);

                    if (CurrentSpeeds.arraySize > 0)
                        RemoveArrayElement(CurrentSpeeds, index);
                }

                /// <summary>
                /// Removes the specified array element from the `property`.
                /// <para></para>
                /// If the element is not at its default value, the first call to
                /// <see cref="SerializedProperty.DeleteArrayElementAtIndex"/> will only reset it, so this method will
                /// call it again if necessary to ensure that it actually gets removed.
                /// </summary>
                protected static void RemoveArrayElement(SerializedProperty property, int index)
                {
                    var count = property.arraySize;
                    property.DeleteArrayElementAtIndex(index);
                    if (property.arraySize == count)
                        property.DeleteArrayElementAtIndex(index);
                }

                /************************************************************************************************************************/

                /// <summary>
                /// Called when reordering states in the list to ensure that any other relevant arrays have their
                /// corresponding elements reordered as well.
                /// </summary>
                protected virtual void OnReorderList(ReorderableList list, int oldIndex, int newIndex)
                {
                    CurrentSpeeds.MoveArrayElement(oldIndex, newIndex);

                    var syncCount = CurrentSynchroniseChildren.arraySize;
                    if (Math.Max(oldIndex, newIndex) >= syncCount)
                    {
                        CurrentSynchroniseChildren.arraySize++;
                        CurrentSynchroniseChildren.GetArrayElementAtIndex(syncCount).boolValue = true;
                        CurrentSynchroniseChildren.arraySize = newIndex + 1;
                    }

                    CurrentSynchroniseChildren.MoveArrayElement(oldIndex, newIndex);
                }

#if !UNITY_2018_1_OR_NEWER
                private int _SelectedIndex;

                private void OnListSelectionChanged(ReorderableList list)
                {
                    _SelectedIndex = list.index;
                }

                private void OnReorderList(ReorderableList list)
                {
                    OnReorderList(list, _SelectedIndex, list.index);
                }
#endif

                /************************************************************************************************************************/

                /// <summary>
                /// Calls <see cref="TryCollapseSpeeds"/> and <see cref="TryCollapseSync"/>.
                /// </summary>
                public static void TryCollapseArrays()
                {
                    TryCollapseSpeeds();
                    TryCollapseSync();
                }

                /************************************************************************************************************************/

                /// <summary>
                /// If every element in the <see cref="CurrentSpeeds"/> array is 1, this method sets the array size to 0.
                /// </summary>
                public static void TryCollapseSpeeds()
                {
                    var property = CurrentSpeeds;
                    var speedCount = property.arraySize;
                    if (speedCount <= 0)
                        return;

                    for (int i = 0; i < speedCount; i++)
                    {
                        if (property.GetArrayElementAtIndex(i).floatValue != 1)
                            return;
                    }

                    property.arraySize = 0;
                }

                /************************************************************************************************************************/

                /// <summary>
                /// Removes any true elements from the end of the <see cref="CurrentSynchroniseChildren"/> array.
                /// </summary>
                public static void TryCollapseSync()
                {
                    var property = CurrentSynchroniseChildren;
                    var count = property.arraySize;
                    var changed = false;

                    for (int i = count - 1; i >= 0; i--)
                    {
                        if (property.GetArrayElementAtIndex(i).boolValue)
                        {
                            count = i;
                            changed = true;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (changed)
                        property.arraySize = count;
                }

                /************************************************************************************************************************/
            }

            /************************************************************************************************************************/
#endif
            #endregion
            /************************************************************************************************************************/
        }

        /************************************************************************************************************************/
        #endregion
        /************************************************************************************************************************/
    }
}

