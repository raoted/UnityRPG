﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("RPG and MMO UI/Icon Slot")]
public class RnMUI_IconSlot : MonoBehaviour
{
	public enum InternalType
	{
		Normal,
		Temporary,
	}
	
	public enum HoverEffectType
	{
		None,
		Color,
		Sprite,
	}
	
	public enum PressEffectType
	{
		None,
		Color,
		Sprite,
	}

	public UITexture iconSprite;
	public bool dragAndDropEnabled = true;
	public bool IsStatic = false;
	public bool AllowThrowAway = true;

	public HoverEffectType hoverEffectType = HoverEffectType.None;
	public UISprite hoverEffectSprite;
	public Color hoverEffectColor = Color.white;
	public float hoverEffectSpeed = 0.15f;
	
	public PressEffectType pressEffectType = PressEffectType.None;
	public UISprite pressEffectSprite;
	public Color pressEffectColor = Color.black;
	public float pressEffectSpeed = 0.15f;
	public bool pressEffectInstaOut = true;

	protected Transform mParent;
	protected UIRoot mRoot;
	protected int mTouchID = int.MinValue;
	protected bool mPressed = false;
	protected bool mDragging = false;
	protected GameObject mTemporaryDraggingPanel;
	protected GameObject mDraggedObject;
	
	protected virtual void Awake()
	{
		this.mParent = this.transform.parent;
		this.mRoot = NGUITools.FindInParents<UIRoot>(this.mParent);
		
		if (this.iconSprite == null)
			this.iconSprite = this.GetComponentInChildren<UITexture>();
	}
	
	protected virtual void Start()
	{
		// Check if we have no icon
		if (this.iconSprite == null)
		{
			Debug.LogWarning(this.GetType() + " requires that you define a icon sprite in order to work.", this);
			this.enabled = false;
			return;
		}

		// Hide the hover and press effects sprites if assigned
		if (this.hoverEffectSprite != null)
			this.hoverEffectSprite.alpha = 0f;
		
		if (this.pressEffectSprite != null)
			this.pressEffectSprite.alpha = 0f;
	}
	
	/// <summary>
	/// Determines whether this slot is assigned.
	/// </summary>
	/// <returns><c>true</c> if this instance is assigned; otherwise, <c>false</c>.</returns>
	public virtual bool IsAssigned()
	{
		return (this.GetIcon() != null);
	}
	
	/// <summary>
	/// Assign the specified slot by icon.
	/// </summary>
	/// <param name="icon">Icon.</param>
	public bool Assign(Texture icon)
	{
		if (icon == null)
			return false;
		
		// Unassign this slot
		this.Unassign();

		// Set the icon
		this.SetIcon(icon);

		return true;
	}

	/// <summary>
	/// Assign the specified slot by source object.
	/// </summary>
	/// <param name="source">Source.</param>
	public virtual bool Assign(Object source)
	{
		if (source is Texture)
		{
			return this.Assign(source as Texture);
		}
		else if (source is RnMUI_IconSlot)
		{
			RnMUI_IconSlot sourceSlot = source as RnMUI_IconSlot;

			if (sourceSlot != null)
				return this.Assign(sourceSlot.GetIcon());
		}

		return false;
	}
	
	/// <summary>
	/// Unassign this slot.
	/// </summary>
	public virtual void Unassign()
	{
		// Remove the icon
		this.SetIcon(null);
	}

	/// <summary>
	/// Gets the icon of this slot if it's set.
	/// </summary>
	/// <returns>The icon.</returns>
	public Texture GetIcon()
	{
		return this.iconSprite.mainTexture;
	}
	
	/// <summary>
	/// Sets the icon of this slot.
	/// </summary>
	/// <param name="iconTex">The icon texture.</param>
	public void SetIcon(Texture iconTex)
	{
		this.iconSprite.mainTexture = iconTex;
		
		if (iconTex != null && !this.iconSprite.gameObject.activeSelf) this.iconSprite.gameObject.SetActive(true);
		if (iconTex == null && this.iconSprite.gameObject.activeSelf) this.iconSprite.gameObject.SetActive(false);
	}
	
	/// <summary>
	/// Raises the hover event.
	/// </summary>
	/// <param name="isOver">If set to <c>true</c> is over.</param>
	public virtual void OnHover(bool isOver)
	{
		if (this.hoverEffectType == HoverEffectType.Sprite)
		{
			if (this.hoverEffectSprite != null)
				TweenAlpha.Begin(this.hoverEffectSprite.gameObject, this.hoverEffectSpeed, ((isOver && !this.mPressed) ? 1f : 0f));
		}
		else if (this.hoverEffectType == HoverEffectType.Color)
		{
			if (this.IsAssigned())
				TweenColor.Begin(this.iconSprite.gameObject, this.hoverEffectSpeed, ((isOver && !this.mPressed) ? this.hoverEffectColor : Color.white));
		}
	}
	
	/// <summary>
	/// Raises the press event.
	/// </summary>
	/// <param name="isDown">If set to <c>true</c> is down.</param>
	public virtual void OnPress(bool isDown)
	{
		this.mPressed = isDown;
		
		if (this.pressEffectType == PressEffectType.Sprite)
		{
			if (this.pressEffectSprite != null)
				TweenAlpha.Begin(this.pressEffectSprite.gameObject, ((this.pressEffectInstaOut) ? 0f : this.pressEffectSpeed), (isDown ? 1f : 0f));
		}
		else if (this.pressEffectType == PressEffectType.Color)
		{
			if (this.IsAssigned())
				TweenColor.Begin(this.iconSprite.gameObject, ((!isDown && this.pressEffectInstaOut) ? 0f : this.pressEffectSpeed), (isDown ? this.pressEffectColor : Color.white));
		}
		
		this.OnHover(((isDown) ? false : UICamera.IsHighlighted(this.gameObject)));
	}

	/// <summary>
	/// Raises the click event.
	/// </summary>
	public virtual void OnClick() { }

	/// <summary>
	/// Raises the tooltip event.
	/// </summary>
	/// <param name="show">If set to <c>true</c> show.</param>
	public virtual void OnTooltip(bool show) { }

	/// <summary>
	/// Raises the drag start event.
	/// </summary>
	protected virtual void OnDragStart()
	{
		if (!this.enabled || !this.IsAssigned() || !this.dragAndDropEnabled)
			return;
		
		this.mDragging = true;
		this.mTouchID = UICamera.currentTouchID;

		// Disable the hover and pressed states
		//this.OnPress(false);
		//this.OnHover(false);
			
		// Create and get temporary slot
		GameObject icon = this.CreateTemporary();
		
		// Save the temporary object
		this.mDraggedObject = icon;

		// Notify the widgets that the parent has changed
		NGUITools.MarkParentAsChanged(this.gameObject);
	}
	
	/// <summary>
	/// Perform the dragging.
	/// </summary>
	protected virtual void OnDrag(Vector2 delta)
	{
		if (!this.mDragging || !this.enabled || this.mTouchID != UICamera.currentTouchID) return;
		this.OnDragDropMove(delta * this.mRoot.pixelSizeAdjustment);
	}
	
	/// <summary>
	/// Adjust the dragged object's position.
	/// </summary>
	protected virtual void OnDragDropMove(Vector2 delta)
	{
		if (this.mDraggedObject != null)
			this.mDraggedObject.transform.localPosition += (Vector3)delta;
	}
	
	/// <summary>
	/// Notification sent when the drag event has ended.
	/// </summary>
	protected virtual void OnDragEnd()
	{
		if (!this.enabled || this.mTouchID != UICamera.currentTouchID) return;
		this.StopDragging(UICamera.hoveredObject);
	}
	
	/// <summary>
	/// Drop the dragged item.
	/// </summary>
	public virtual void StopDragging(GameObject go)
	{
		if (this.mDragging)
		{
			this.mDragging = false;
			this.OnDragDropRelease(go);
		}
	}
	
	/// <summary>
	/// Raises the drag drop release event.
	/// </summary>
	/// <param name="surface">Surface.</param>
	protected virtual void OnDragDropRelease(GameObject surface)
	{
		// Destroy the temporary icon
		if (this.mTemporaryDraggingPanel != null)
		{
			NGUITools.Destroy(this.mTemporaryDraggingPanel);
		}
		else if (this.mDraggedObject != null)
		{
			NGUITools.Destroy(this.mDraggedObject);
		}
		
		// Check if we have no surface
		if (surface == null)
		{
			// No surface found
			// Try to throw away the slot content
			this.OnThrowAway();
			return;
		}
		
		// Try getting a target slot
		RnMUI_IconSlot targetSlot = surface.GetComponent<RnMUI_IconSlot>();

		// Check if we have a target slot
		if (targetSlot == null)
		{
			// No target slot
			// Try to throw away the slot content
			this.OnThrowAway();
			return;
		}

		// Check if the target slot has drag and drop enabled
		if (targetSlot.dragAndDropEnabled)
		{
			// Normal empty slot assignment
			if (!targetSlot.IsAssigned())
			{
				// Assign the target slot with the info from this one
				if (targetSlot.Assign(this) && !this.IsStatic)
					this.Unassign();
			}
			// The target slot is assigned
			else
			{
				// If the target slot is not static
				// and this slot is not static
				if (!targetSlot.IsStatic && !this.IsStatic)
				{
					// Check if we can swap
					if (this.CanSwapWith(targetSlot))
					{
						// Swap the slots
						this.PerformSlotSwap(targetSlot);
					}
				}
				// If the target slot is not static
				// and this slot is a static one
				else if (!targetSlot.IsStatic && this.IsStatic)
				{
					targetSlot.Assign(this);
				}
			}
		}
	}
	
	/// <summary>
	/// Determines whether this slot can swap with the specified target slot.
	/// </summary>
	/// <returns><c>true</c> if this instance can swap with the specified target; otherwise, <c>false</c>.</returns>
	/// <param name="target">Target.</param>
	public virtual bool CanSwapWith(Object target)
	{
		return (target is RnMUI_IconSlot);
	}
	
	/// <summary>
	/// Performs a slot swap.
	/// </summary>
	/// <param name="targetObject">Target slot.</param>
	public virtual bool PerformSlotSwap(Object targetObject)
	{
		// Get the source slot
		RnMUI_IconSlot targetSlot = (targetObject as RnMUI_IconSlot);
		
		// Get the target slot icon
		Texture targetIcon = targetSlot.GetIcon();
		
		// Assign the target slot with this one
		bool assign1 = targetSlot.Assign(this);
		
		// Assign this slot by the target slot icon
		bool assign2 = this.Assign(targetIcon);
		
		// Return the status
		return (assign1 && assign2);
	}
	
	/// <summary>
	/// This method is raised to confirm throwing away the slot.
	/// </summary>
	protected virtual void OnThrowAway()
	{
		// Check if throw away is enabled
		if (!this.AllowThrowAway)
		{
			this.OnThrowAwayDenied();
			return;
		}
		
		// Unassign the slot
		this.Unassign();
	}

	/// <summary>
	/// This method is raised when the slot is denied to be thrown away.
	/// </summary>
	protected virtual void OnThrowAwayDenied() { }

	protected virtual GameObject CreateTemporary()
	{
		// Create temporary icon
		GameObject icon = (GameObject)Instantiate(this.iconSprite.gameObject);
		icon.layer = this.gameObject.layer;
		
		// Check if the icon has tweens on it
		TweenColor cTween = icon.GetComponent<TweenColor>();
		if (cTween != null)
		{
			cTween.enabled = false;
			NGUITools.Destroy(cTween);
		}
		
		// Apply normal color to the clone icon
		UITexture tex = icon.GetComponent<UITexture>();
		if (tex != null)
		{
			tex.color = Color.white;
		}
		
		// Re-parent the item
		if (UIDragDropRoot.root != null)
		{
			icon.transform.parent = UIDragDropRoot.root;
		}
		else
		{
			// Create temporary panel
			GameObject panelObj = new GameObject("_TemporaryPanel");
			panelObj.layer = this.gameObject.layer;
			panelObj.transform.parent = NGUITools.GetRoot(this.gameObject).transform;
			panelObj.transform.localScale = Vector3.one;
			panelObj.transform.localRotation = Quaternion.identity;
			panelObj.transform.localPosition = Vector3.one;
			
			// Save the panel so we can destroy it
			this.mTemporaryDraggingPanel = panelObj;
			
			// Apply depth
			UIPanel panel = panelObj.AddComponent<UIPanel>();
			panel.depth = UIPanel.nextUnusedDepth;
			icon.transform.parent = panelObj.transform;
		}
		
		// Fix it's position
		icon.transform.position = NGUITools.FindCameraForLayer(this.gameObject.layer).ScreenToWorldPoint(Input.mousePosition);
		icon.transform.rotation = this.transform.rotation;
		icon.transform.localScale = this.transform.localScale;

		return icon;
	}
}

