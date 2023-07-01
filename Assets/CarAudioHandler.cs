using System;
using System.Collections;
using System.Collections.Generic;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;

[RequireComponent(typeof(CarController))]
public class CarAudioHandler : MonoBehaviour
{
   private CarController _carController;
   private EventInstance _carEngineSoundInstance;
   

   [SerializeField] private EventReference carEngineSoundReference;
   [SerializeField] private EventReference carCrashSoundReference;
   [SerializeField] private ParamRef rpm;
   [SerializeField] private ParamRef accelerationInput;
   [SerializeField] private ParamRef impulse;
   [SerializeField] private Transform audioEmitterTransform;


   private void Awake()
   {
      _carController = GetComponent<CarController>();
      _carEngineSoundInstance = RuntimeManager.CreateInstance(carEngineSoundReference);
     
   }

   private void Start()
   {
      RuntimeManager.AttachInstanceToGameObject(_carEngineSoundInstance, audioEmitterTransform);
      _carEngineSoundInstance.start();
   }

   private void Update()
   {
      _carEngineSoundInstance.setParameterByName(rpm.Name, Sigmoid(Mathf.Abs(_carController.Rpm)));
      _carEngineSoundInstance.setParameterByName(accelerationInput.Name, Mathf.Abs(_carController.AccelerationInput));
      
   }

   private void OnCollisionEnter(Collision other)
   { 
      var crashInstance = RuntimeManager.CreateInstance(carCrashSoundReference);
     
      var result = crashInstance.setParameterByName(impulse.Name, other.impulse.magnitude/16000);
      Debug.Log($"{other.impulse.magnitude/16000} {result}");
      crashInstance.set3DAttributes( other.GetContact(0).point.To3DAttributes());
      crashInstance.start();
      crashInstance.release();
   }

   private float Sigmoid(float x) =>  1 - 1 / (1 + x/2000);
}
