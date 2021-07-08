using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Audio;

using UnityEditor;

using NeedsVsWants.EditorUtilities;

namespace NeedsVsWants.Audio
{
    [CreateAssetMenu(menuName = "NeedsVsWants/Audio/Audio Asset")]
    public class AudioAsset : ScriptableObject
    {
        [SerializeField]
        AudioClip _AudioClip;

        [SerializeField]
        AudioMixerGroup _OutputAudioMixerGroup;

        [SerializeField]
        int _Priority = 128;

        [SerializeField]
        float _Volume = 1;
        [SerializeField]
        float _Pitch = 1;
        
        [SerializeField]
        FloatMinMax _RandomPitch;

        [SerializeField]
        bool _IsPitchRandom = false;

        #if UNITY_EDITOR
        [CustomEditor(typeof(AudioAsset), true)]
        class AudioAssetCustomEditor : Editor
        {
            bool _IsPitchFoldout = false;

            AudioSource _PreviewAudioSource;

            void OnEnable() 
            {
                _PreviewAudioSource = EditorUtility.CreateGameObjectWithHideFlags("Audio Preview", HideFlags.HideAndDontSave, typeof(AudioSource)).GetComponent<AudioSource>();
            }

            void OnDisable() 
            {
                DestroyImmediate(_PreviewAudioSource.gameObject);    
            }

            public override void OnInspectorGUI()
            {
                SerializedProperty priorityProperty = serializedObject.FindProperty("_Priority");
                SerializedProperty volumeProperty = serializedObject.FindProperty("_Volume");
                SerializedProperty isPitchRandomProperty = serializedObject.FindProperty("_IsPitchRandom");
                SerializedProperty pitchProperty = serializedObject.FindProperty("_Pitch");
                SerializedProperty randomPitchMinProperty = serializedObject.FindProperty("_RandomPitch").FindPropertyRelative("min");
                SerializedProperty randomPitchMaxProperty = serializedObject.FindProperty("_RandomPitch").FindPropertyRelative("max");

                float randomPitchMin = randomPitchMinProperty.floatValue;
                float randomPitchMax = randomPitchMaxProperty.floatValue;

                EditorGUILayout.ObjectField(serializedObject.FindProperty("_AudioClip"));

                EditorGUILayout.ObjectField(serializedObject.FindProperty("_OutputAudioMixerGroup"));

                priorityProperty.intValue = EditorGUILayout.IntSlider("Priority", priorityProperty.intValue, 0, 256); 

                volumeProperty.floatValue = EditorGUILayout.Slider("Volume", volumeProperty.floatValue, 0, 1);           

                _IsPitchFoldout = EditorGUILayout.Foldout(_IsPitchFoldout, "Pitch");

                if(_IsPitchFoldout)
                {
                    isPitchRandomProperty.boolValue = EditorGUILayout.Toggle("Is Random", isPitchRandomProperty.boolValue);

                    if(!isPitchRandomProperty.boolValue)
                        pitchProperty.floatValue = EditorGUILayout.Slider(pitchProperty.floatValue, -3, 3);

                    else
                    {
                        EditorGUILayout.BeginHorizontal();
                        randomPitchMin = Mathf.Clamp(EditorGUILayout.FloatField(randomPitchMin), -3, randomPitchMax);
                        EditorGUILayout.MinMaxSlider(ref randomPitchMin, ref randomPitchMax, -3, 3);
                        randomPitchMax = Mathf.Clamp(EditorGUILayout.FloatField(randomPitchMax), randomPitchMin, 3);
                        EditorGUILayout.EndHorizontal();

                        randomPitchMinProperty.floatValue = randomPitchMin;
                        randomPitchMaxProperty.floatValue = randomPitchMax;
                    }
                }

                if(GUILayout.Button("Preview"))
                    (target as AudioAsset).Play(_PreviewAudioSource);

                serializedObject.ApplyModifiedProperties();


                // AudioAsset audioAsset = target as AudioAsset;

                // float randomPitchMin, randomPitchMax;

                // if(!audioAsset)
                //     return;

                // randomPitchMin = audioAsset._RandomPitch.min;
                // randomPitchMax = audioAsset._RandomPitch.max;
                    
                // // Audio Clip
                // audioAsset._AudioClip = Functions.DrawObjectField<AudioClip>("Audio Clip", audioAsset._AudioClip, false);
                
                // // Output
                // audioAsset._OutputAudioMixerGroup = Functions.DrawObjectField<AudioMixerGroup>("Output", audioAsset._OutputAudioMixerGroup, false);
                
                // // Priority
                // audioAsset._Priority = EditorGUILayout.IntSlider("Priority", audioAsset._Priority, 0, 256);          

                // // Volume
                // audioAsset._Volume = EditorGUILayout.Slider("Volume", audioAsset._Volume, 0, 1);     
                
                // // Pitch
                // _IsPitchFoldout = EditorGUILayout.Foldout(_IsPitchFoldout, "Pitch");

                // if(_IsPitchFoldout)
                // {
                //     audioAsset._IsPitchRandom = EditorGUILayout.Toggle("Is Random", audioAsset._IsPitchRandom);

                //     if(!audioAsset._IsPitchRandom)
                //         audioAsset._Pitch = EditorGUILayout.Slider(audioAsset._Pitch, -3, 3);

                //     else
                //     {
                //         EditorGUILayout.BeginHorizontal();
                //         randomPitchMin = Mathf.Clamp(EditorGUILayout.FloatField(randomPitchMin), -3, randomPitchMax);
                //         EditorGUILayout.MinMaxSlider(ref randomPitchMin, ref randomPitchMax, -3, 3);
                //         randomPitchMax = Mathf.Clamp(EditorGUILayout.FloatField(randomPitchMax), randomPitchMin, 3);
                //         EditorGUILayout.EndHorizontal();

                //         audioAsset._RandomPitch.min = randomPitchMin;
                //         audioAsset._RandomPitch.max = randomPitchMax;
                //     }
                // }

                // if(GUILayout.Button("Preview"))
                //     audioAsset.Play(_PreviewAudioSource);

                // serializedObject.ApplyModifiedProperties();
            }
        }
        #endif

        public void Play(AudioSource audioSource)
        {
            audioSource.Stop();

            audioSource.clip = _AudioClip;
            audioSource.outputAudioMixerGroup = _OutputAudioMixerGroup;
            audioSource.priority = _Priority;
            audioSource.volume = _Volume;
            audioSource.pitch = _IsPitchRandom ? Random.Range(_RandomPitch.min, _RandomPitch.max) : _Pitch;

            audioSource.Play();
        }

        public void PlayOneShot(AudioSource audioSource) =>
            PlayOneShot(audioSource, 1);
            
        public void PlayOneShot(AudioSource audioSource, float volumeScale)
        {
            audioSource.clip = _AudioClip;
            audioSource.outputAudioMixerGroup = _OutputAudioMixerGroup;
            audioSource.priority = _Priority;
            audioSource.volume = _Volume;
            audioSource.pitch = _IsPitchRandom ? Random.Range(_RandomPitch.min, _RandomPitch.max) : _Pitch;
            
            audioSource.PlayOneShot(_AudioClip, volumeScale);
        }
    }
}