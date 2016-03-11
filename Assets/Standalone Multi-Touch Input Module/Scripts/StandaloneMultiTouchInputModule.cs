/* Alot of this code is from Unity's Source code at https://bitbucket.org/Unity-Technologies/ui/src */
/* Modified by Kaz Crowe */
/* StandaloneMultiTouchInputModule.cs ver. 1.0 */
using System.Text;

namespace UnityEngine.EventSystems
{
	[AddComponentMenu("Event/Standalone Multi-Touch Input Module")]
	public class StandaloneMultiTouchInputModule : PointerInputModule
	{
		protected StandaloneMultiTouchInputModule()
		{ }

		/* Controller Bools */
		private bool canUseTouch = true;
		private bool canUseMouse = false;

		// This function is called from Unity's EventSystem
		public override void Process ()
		{
			// If we find any touches
			if( Input.touchCount > 0 && canUseTouch == true )
			{
				// Since we have found some touches, then disable the ability to use our mouse
				if( canUseMouse == true )
					canUseMouse = false;

				// Then we want to process our Touch Events
				ProcessTouchEvents();
			}
			// Else we don't have any touches on the screen
			else
			{
				// If we currently can't use our mouse, then we need to change that
				if( canUseMouse == false )
					canUseMouse = true;
			}
			// If we find that we are attempting to use our mouse, check if we can
			if( ( Input.GetMouseButton( 0 ) || Input.GetMouseButtonUp( 0 ) ) && canUseMouse == true )
			{
				// Since we found some mouse input, disable our touch
				if( canUseTouch == true )
					canUseTouch = false;

				// And then process our mouse as our input
				ProcessMouseEvent();
			}
			// Else we aren't using our mouse
			else
			{
				// So if we can't currently use touch, then enable it
				if( canUseTouch == false )
					canUseTouch = true;
			}
		}

		private void ProcessTouchEvents ()
		{
			// We want to get all of the different touches on our screen
			for( int i = 0; i < Input.touchCount; ++i )
			{
				// We will store the current touch
				Touch touch = Input.GetTouch( i );

				// Create a few bools to store our touch info
				bool released;
				bool pressed;

				// Get our pointer and call GetTouchPointerEventData which is inherited from PointerInputModule
				PointerEventData pointer = GetTouchPointerEventData( touch, out pressed, out released );

				// Call to Process our TouchPress with our modified bools from GetTouchPointerEventData
				ProcessTouchPress( pointer, pressed, released );

				// If we have not released, then call ProcessMove and Drag from our inherited PointerInputModule
				if ( !released )
				{
					ProcessMove( pointer );
					ProcessDrag( pointer );
				}
				// Else we need to remove our PointerData
				else
					RemovePointerData( pointer );
			}
		}

		private void ProcessTouchPress ( PointerEventData pointerEvent, bool pressed, bool released )
		{
			var currentOverGo = pointerEvent.pointerCurrentRaycast.gameObject;

			// PointerDown notification
			if( pressed )
			{
				pointerEvent.eligibleForClick = true;
				pointerEvent.delta = Vector2.zero;
				pointerEvent.dragging = false;
				pointerEvent.useDragThreshold = true;
				pointerEvent.pressPosition = pointerEvent.position;
				pointerEvent.pointerPressRaycast = pointerEvent.pointerCurrentRaycast;
				
				DeselectIfSelectionChanged( currentOverGo, pointerEvent );

				if( pointerEvent.pointerEnter != currentOverGo )
				{
					// send a pointer enter to the touched element if it isn't the one to select...
					HandlePointerExitAndEnter( pointerEvent, currentOverGo );
					pointerEvent.pointerEnter = currentOverGo;
				}

				// search for the control that will receive the press
				var newPressed = ExecuteEvents.ExecuteHierarchy( currentOverGo, pointerEvent, ExecuteEvents.pointerDownHandler );

				// if we can't find a press handler set the press handler to be what would receive a click
				if( newPressed == null )
					newPressed = ExecuteEvents.GetEventHandler<IPointerClickHandler>( currentOverGo );
				
				pointerEvent.pointerPress = newPressed;
				pointerEvent.rawPointerPress = currentOverGo;

				// Save the drag handler as well
				pointerEvent.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>( currentOverGo );
				
				if( pointerEvent.pointerDrag != null )
					ExecuteEvents.Execute( pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.initializePotentialDrag );
			}
			
			if( released )
			{
				ExecuteEvents.Execute( pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerUpHandler );
				
				var pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>( currentOverGo );
				
				if( pointerEvent.pointerPress == pointerUpHandler && pointerEvent.eligibleForClick )
					ExecuteEvents.Execute( pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerClickHandler );
				else if( pointerEvent.pointerDrag != null )
					ExecuteEvents.ExecuteHierarchy( currentOverGo, pointerEvent, ExecuteEvents.dropHandler );
				
				pointerEvent.eligibleForClick = false;
				pointerEvent.pointerPress = null;
				pointerEvent.rawPointerPress = null;
				
				if( pointerEvent.pointerDrag != null && pointerEvent.dragging )
					ExecuteEvents.Execute( pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.endDragHandler );
				
				pointerEvent.dragging = false;
				pointerEvent.pointerDrag = null;
				
				if( pointerEvent.pointerDrag != null )
					ExecuteEvents.Execute( pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.endDragHandler );
				
				pointerEvent.pointerDrag = null;
				
				ExecuteEvents.ExecuteHierarchy( pointerEvent.pointerEnter, pointerEvent, ExecuteEvents.pointerExitHandler );
				pointerEvent.pointerEnter = null;
			}
		}

		private void ProcessMouseEvent()
		{
			var mouseData = GetMousePointerEventData();
			
			var leftButtonData = mouseData.GetButtonState( PointerEventData.InputButton.Left ).eventData;
			
			// Process the first mouse button
			ProcessMousePress( leftButtonData );
			ProcessMove( leftButtonData.buttonData );
			ProcessDrag( leftButtonData.buttonData );
		}

		private void ProcessMousePress ( MouseButtonEventData data )
		{
			var pointerEvent = data.buttonData;
			var currentOverGo = pointerEvent.pointerCurrentRaycast.gameObject;
			
			// PointerDown notification
			if( data.PressedThisFrame() )
			{
				// search for the control that will receive the press
				// if we can't find a press handler set the press
				// handler to be what would receive a click.
				var newPressed = ExecuteEvents.ExecuteHierarchy( currentOverGo, pointerEvent, ExecuteEvents.pointerDownHandler );
				
				// didnt find a press handler... search for a click handler
				if( newPressed == null )
					newPressed = ExecuteEvents.GetEventHandler<IPointerClickHandler>( currentOverGo );
				
				pointerEvent.pointerPress = newPressed;
				pointerEvent.rawPointerPress = currentOverGo;
				
				// Save the drag handler as well
				pointerEvent.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>( currentOverGo );
				
				if( pointerEvent.pointerDrag != null )
					ExecuteEvents.Execute( pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.initializePotentialDrag );
			}
			
			// PointerUp notification
			if( data.ReleasedThisFrame() )
			{
				ExecuteEvents.Execute( pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerUpHandler );
				
				// see if we mouse up on the same element that we clicked on...
				var pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>( currentOverGo );
				
				// PointerClick and Drop events
				if( pointerEvent.pointerPress == pointerUpHandler )
					ExecuteEvents.Execute( pointerEvent.pointerPress, pointerEvent, ExecuteEvents.pointerClickHandler );
				else if( pointerEvent.pointerDrag != null )
					ExecuteEvents.ExecuteHierarchy( currentOverGo, pointerEvent, ExecuteEvents.dropHandler );

				pointerEvent.pointerPress = null;
				pointerEvent.rawPointerPress = null;
				
				if( pointerEvent.pointerDrag != null )
					ExecuteEvents.Execute( pointerEvent.pointerDrag, pointerEvent, ExecuteEvents.endDragHandler );

				pointerEvent.pointerDrag = null;
			}
		}
	}
}