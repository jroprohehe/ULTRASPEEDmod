using UMM;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

namespace ULTRASPEED
{
#pragma warning disable CS0618 //disables annoying "Class WWW is deprecated" warning
#pragma warning disable CS0114 //also disables something

    [UKPlugin(PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION, "gas gas gas", false, true)]
    public class Plugin : UKMod
    {
        private static bool CheckSumEnabled = true; //change this variable to false if you dont want checksum to be enabled
        private int CurrentSpeed = 1000;
        private GameObject PlayerObject;
        private AudioSource GASGASGASSource;


        public virtual void OnModLoaded()
        {
            //Debug.Log("file:///" + Environment.CurrentDirectory + @"\BepInEx\UMM Mods\ULTRASPEED\" + "gasgasgas.mp3");
            Debug.Log($"Plugin {PluginInfo.PLUGIN_NAME} is loaded!");
            
        }

        void Update()
        {
            if (NewMovement.Instance != null && CameraController.Instance != null)
            {
                NewMovement.Instance.walkSpeed = CurrentSpeed;

                if (NewMovement.Instance.dead == true && CurrentSpeed == 10000) //turns off if player is dead
                {
                    CameraController.Instance.defaultFov = CameraController.Instance.defaultFov - 30;
                    GASGASGASSource.Stop();
                    CurrentSpeed = 1000;

                }

                if (SceneManager.GetActiveScene().name != "Main Menu" && SceneManager.GetActiveScene().name != "Intro") //creates AudioSource component for the gasgasgas music if player isnt in main menu or intro
                {
                    PlayerObject = GameObject.Find("Player");

                    if (GASGASGASSource == null)
                    {
                        GASGASGASSource = PlayerObject.transform.Find("Main Camera").gameObject.AddComponent<AudioSource>();
                        
                        
                    }

                }

                if (Input.GetKeyDown(KeyCode.LeftAlt) && GASGASGASSource != null && MusicManager.Instance != null && NewMovement.Instance != null && NewMovement.Instance.dead == false) //turns on/off gasgasgas mode
                {
                    switch (CurrentSpeed)
                    {
                        case 1000: //turns on gasgasgas mode
                            StartCoroutine(LoadSoundIntoSourceAndPlay("gasgasgas.mp3", GASGASGASSource, 95, 16777728));
                            MusicManager.Instance.StopMusic();
                            CurrentSpeed = 10000;
                            CameraController.Instance.defaultFov = CameraController.Instance.defaultFov + 30;
                            break;
                        case 10000: //turns off gasgasgas mode
                            if(SceneManager.GetActiveScene().name != "uk_construct") { MusicManager.Instance.StartMusic(); }
                            GASGASGASSource.Stop();
                            CurrentSpeed = 1000;
                            CameraController.Instance.defaultFov = CameraController.Instance.defaultFov - 30;
                            break;
                    }
                }
            }
        }

        private int GetClipSize(AudioClip audioClp)
        {
            return ((audioClp.samples * audioClp.channels) * 2);
        }

        private IEnumerator LoadSoundIntoSourceAndPlay(string soundName, AudioSource audioSource, int audioLenght, int audioSize) //basic audio loader
        {
            Debug.Log("loading audio " + soundName);

            WWW AudioFile = new WWW("file:///" + Environment.CurrentDirectory + @"\BepInEx\UMM Mods\ULTRASPEED\" + soundName);

            if(AudioFile.error != null)
            {
                Debug.Log(AudioFile.error);
                
            }
            else
            {
                audioSource.clip = AudioFile.GetAudioClip(false, true, AudioType.MPEG);

                while(!AudioFile.isDone)
                {
                    yield return null;
                }

                if (Math.Round(audioSource.clip.length) == audioLenght && GetClipSize(audioSource.clip) == audioSize) //poorly made checksum
                {
                    GASGASGASSource.loop = true;
                    GASGASGASSource.enabled = true;
                    GASGASGASSource.Play();
                    Debug.Log(GetClipSize(audioSource.clip) + "  - audio size");

                }
                else if(CheckSumEnabled == false)
                {
                    GASGASGASSource.loop = true;
                    GASGASGASSource.enabled = true;
                    GASGASGASSource.Play();
                    
                }
                else
                {
                    Debug.LogError("DONT FUCKING CHANGE FILES OF MY MOD"); //....

                }

            }

        }
    }
}
