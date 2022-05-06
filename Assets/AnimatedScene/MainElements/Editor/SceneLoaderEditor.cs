using UnityEngine;
using UnityEditor;
using DG.Tweening;
using UnityEditor.SceneManagement;

namespace FM{
    [CustomEditor(typeof(SceneLoader))]
    public class SceneLoaderEditor : Editor {

        SceneLoader loader;

        void OnEnable(){
            loader = (SceneLoader)target;
        }

        public override void OnInspectorGUI(){
            EditorGUILayout.LabelField("Entry", EditorStyles.boldLabel);
            loader.durationEntry = EditorGUILayout.FloatField("Entry Duration", loader.durationEntry);
            loader.easeEntry = (Ease) EditorGUILayout.EnumPopup("Entry Ease", loader.easeEntry);

            EditorGUILayout.Space();
            loader.animationTypeEntry = (AnimationType) EditorGUILayout.EnumPopup("Entry Animation Type", loader.animationTypeEntry);

            if(loader.animationTypeEntry == AnimationType.Slide){
                loader.slideTypeEntry = (SlideType) EditorGUILayout.EnumPopup("Entry Type", loader.slideTypeEntry);
                loader.slideDirectionEntry = (SlideDirection) EditorGUILayout.EnumPopup("Entry Direction", loader.slideDirectionEntry);
            }else if(loader.animationTypeEntry == AnimationType.Circle){
                loader.circleSlideEntry = (CircleSlide) EditorGUILayout.EnumPopup("Entry Direction", loader.circleSlideEntry);
                if(loader.circleSlideEntry != CircleSlide.Static){
                    loader.circleSizeEntry = EditorGUILayout.FloatField("Exit Size", loader.circleSizeEntry);
                    loader.circleExitEase = (Ease) EditorGUILayout.EnumPopup("Exit Ease", loader.circleExitEase);
                    loader.circleExitDuration = EditorGUILayout.FloatField("Exit Duration", loader.circleExitDuration);
                }
            }

            loader.backPanelColorEntry = EditorGUILayout.ColorField("Entry Color", loader.backPanelColorEntry);
            if(loader.animationTypeEntry == AnimationType.Slide && loader.slideTypeEntry == SlideType.Double){
                loader.frontPanelColorEntry = EditorGUILayout.ColorField("Entry Color Front Panel", loader.frontPanelColorEntry);
            }
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Exit", EditorStyles.boldLabel);
            loader.durationExit = EditorGUILayout.FloatField("Exit Duration", loader.durationExit);
            loader.holdDuration = EditorGUILayout.FloatField("Hold Duration", loader.holdDuration);
            loader.easeExit = (Ease) EditorGUILayout.EnumPopup("Exit Ease", loader.easeExit);
            
            EditorGUILayout.Space();
            loader.animationTypeExit = (AnimationType) EditorGUILayout.EnumPopup("Exit Animation Type", loader.animationTypeExit);

            if(loader.animationTypeExit == AnimationType.Slide){
                loader.slideTypeExit = (SlideType) EditorGUILayout.EnumPopup("Exit Type", loader.slideTypeExit);
                loader.slideDirectionExit = (SlideDirection) EditorGUILayout.EnumPopup("Exit Direction", loader.slideDirectionExit);
            }else if(loader.animationTypeExit == AnimationType.Circle){
                loader.circleSlideExit = (CircleSlide) EditorGUILayout.EnumPopup("Exit Direction", loader.circleSlideExit);
                if(loader.circleSlideExit != CircleSlide.Static){
                    loader.circleSizeExit = EditorGUILayout.FloatField("Entry Size", loader.circleSizeExit);
                    loader.circleEntryEase = (Ease) EditorGUILayout.EnumPopup("Entry Ease", loader.circleEntryEase);
                    loader.circleEntryDuration = EditorGUILayout.FloatField("Entry Duration", loader.circleEntryDuration);
                }
            }

            loader.backPanelColorExit = EditorGUILayout.ColorField("Exit Color", loader.backPanelColorExit);
            if(loader.animationTypeExit == AnimationType.Slide && loader.slideTypeExit == SlideType.Double){
                loader.frontPanelColorExit = EditorGUILayout.ColorField("Exit Color Front Panel", loader.frontPanelColorExit);
            }

            if(GUI.changed)
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            // EditorUtility.SetDirty(loader);
            // serializedObject.ApplyModifiedProperties();
        }

    }
}
