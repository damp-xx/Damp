///////////////////////////////////////////////////////////
//  PlayerMonsterDetect.cs
//  Implementation of the Class PlayerMonsterDetect
//  Generated by Enterprise Architect
//  Created on:      16-apr-2013 11:50:50
//  Original author: Space-Punk
///////////////////////////////////////////////////////////


using System.Collections.Generic;
using CollitionControle;
using Sprites;

namespace CollitionControle {
	public class PlayerMonsterDetect : IColitionDetect {

		public CollitionControle.MonsteDeathEvent m_MonsteDeathEvent;
		public CollitionControle.PlayerDeathEvent m_PlayerDeathEvent;

		public PlayerMonsterDetect(){

		}

		~PlayerMonsterDetect(){

		}

		public virtual void Dispose(){

		}

		/// 
		/// <param name="mSpriteCollection"></param>
		/// <param name="mSprite2"></param>
		/// <param name="mSprite1"></param>


        public List<IEvent> Detect(ISpriteContainerCollition mSpriteCollection, ISprite mSprite2, ISprite mSprite1)
        {
            throw new System.NotImplementedException();
        }
    }//end PlayerMonsterDetect

}//end namespace CollitionControle