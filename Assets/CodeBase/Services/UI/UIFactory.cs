using System;
using System.Collections.Generic;
using CodeBase.Services.Providers;
using CodeBase.Services.StaticDatas;
using CodeBase.UI.Windows;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace CodeBase.Services.UI
{
    public class UIFactory 
    {
        private readonly UIStaticDataService _uiStaticDataService;
        private readonly IObjectResolver _objectResolver;
        private readonly LocationProvider _locationProvider;

        private CameraProvider _cameraProvider;

        public UIFactory(UIStaticDataService uiStaticDataService, IObjectResolver objectResolver, LocationProvider locationProvider, CameraProvider cameraProvider)
        {
            _cameraProvider = cameraProvider;
            _locationProvider = locationProvider;
            _objectResolver = objectResolver;
            _uiStaticDataService = uiStaticDataService;
        }
        
        public T CreateWindow<T>() where T : WindowBase
        {
            var prefab = _uiStaticDataService.GetWindow<T>();

            prefab.GetComponent<Canvas>().worldCamera = _cameraProvider.Camera;

            return _objectResolver.Instantiate<T>(prefab, _locationProvider.UIParent);
        }
        
    }
}