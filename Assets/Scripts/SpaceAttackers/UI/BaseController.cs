using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

namespace SpaceAttackers.UI
{
	public abstract class BaseController : MonoBehaviour
	{
		[SerializeField] private List<string> viewElementNames;

		protected readonly Dictionary<string, VisualElement> VisualElements = new();
		protected readonly Dictionary<string, View> Views = new();
		private readonly List<View> _updatingViews = new();

		private UIDocument _uiDocument;
		private View _currentView;

		public virtual void Awake()
		{
			_uiDocument = GetComponent<UIDocument>();

			foreach (var elementName in viewElementNames)
			{
				VisualElements.Add(elementName, _uiDocument.rootVisualElement.Q<TemplateContainer>(elementName));
			}
		}

		private void Update()
		{
			foreach (var updatingView in _updatingViews)
			{
				updatingView.ViewUpdate();
			}
		}

		private static bool IsOverride(MethodInfo method)
		{
			return method.GetBaseDefinition().DeclaringType != method.DeclaringType;
		}

		protected void InitializeViews()
		{
			foreach (var (_, view) in Views)
			{
				view.GetElements();
				view.RegisterEvents();
				var viewMethod = ((Action)view.ViewUpdate).Method;
				if (IsOverride(viewMethod))
				{
					_updatingViews.Add(view);
				}
			}
		}

		protected void RegisterInitialView(View initialView)
		{
			_currentView = initialView;
			_currentView.ShowView();
		}

		public void SwitchView(string view)
		{
			_currentView.HideView();
			Views[view].ShowView();
			_currentView = Views[view];
		}

		public void UnregisterAllEvents()
		{
			foreach (var (_, view) in Views)
			{
				view.UnregisterEvents();
			}
		}
	}
}