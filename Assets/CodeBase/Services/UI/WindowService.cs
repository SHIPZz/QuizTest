using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.UI.Windows;

namespace CodeBase.Services.UI
{
    public class WindowService
    {
        private readonly UIFactory _uiFactory;
        private Dictionary<Type, WindowBase> _createdWindows = new Dictionary<Type, WindowBase>();

        public WindowBase CurrentWindow { get; private set; }

        public WindowService(UIFactory uiFactory) =>
            _uiFactory = uiFactory;

        public void Open<T>(bool closeCurrentWindow = false) where T : WindowBase
        {
            ClearDestroyedWindows();

            if (CurrentWindow != null && typeof(T) == CurrentWindow.GetType())
                return;

            if (_createdWindows.ContainsKey(typeof(T)))
                return;

            if (closeCurrentWindow)
                CloseCurrentWindow();

            var targetWindow = Get<T>();
            OpenCurrentWindow();
        }

        public bool CompareCurrentWindow<T>() where T : WindowBase
        {
            if (CurrentWindow == null)
                return false;
            
            return typeof(T) == CurrentWindow.GetType();
        }

        public void OpenCurrentWindow()
        {
            CurrentWindow.Open();
        }

        public T Get<T>() where T : WindowBase
        {
            ClearDestroyedWindows();

            if (_createdWindows.ContainsKey(typeof(T)))
            {
                CurrentWindow = (T)_createdWindows[typeof(T)];
                return (T)CurrentWindow;
            }

            WindowBase targetWindow = _uiFactory.CreateWindow<T>();
            CurrentWindow = targetWindow;
            _createdWindows[typeof(T)] = targetWindow;
            return (T)targetWindow;
        }

        public void CloseAll()
        {
            ClearDestroyedWindows();

            foreach (WindowBase window in _createdWindows.Values)
            {
                window.Close();
            }
        }

        public void Close<T>() where T : WindowBase
        {
            ClearDestroyedWindows();

            if (!_createdWindows.TryGetValue(typeof(T), out WindowBase windowBase))
                return;

            windowBase.Close();
            ClearDestroyedWindows();
        }

        public void CloseCurrentWindow() =>
            CurrentWindow.Close();

        private void ClearDestroyedWindows()
        {
            List<Type> windowsToRemove = _createdWindows
                .Where(pair => pair.Value == null)
                .Select(pair => pair.Key)
                .ToList();

            foreach (Type typeToRemove in windowsToRemove)
                _createdWindows.Remove(typeToRemove);
        }
    }
}