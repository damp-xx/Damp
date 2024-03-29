///////////////////////////////////////////////////////////
//  IEventHandlerMain.cs
//  Implementation of the Interface IEventHandlerMain
//  Generated by Enterprise Architect
//  Created on:      16-apr-2013 11:50:26
//  Original author: Space-Punk
///////////////////////////////////////////////////////////


using System.Collections.Generic;
using Collision;

namespace EventHandling {
	public interface IEventHandlerMain  {

		/// 
		/// <param name="mEventList"></param>
		void HandleEvents(List<IEvent> mEventList);
	}//end IEventHandlerMain

}//end namespace EventHandling