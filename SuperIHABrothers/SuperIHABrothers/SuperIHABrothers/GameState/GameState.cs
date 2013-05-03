///////////////////////////////////////////////////////////
//  GameState.cs
//  Implementation of the Class GameState
//  Generated by Enterprise Architect
//  Created on:      16-apr-2013 11:48:51
//  Original author: Space-Punk
///////////////////////////////////////////////////////////




namespace GameState {
	public class GameState : IGameState, IGameStateEvent, IGameStateLevel
	{

	    private int _lifes;
		public GameState(){

		}


		/// 
		/// <param name="mScore"></param>
		public void AddScore(int mScore){

		}

		public int GameLevel{
			get{
				return GameLevel;
			}
			set{
				GameLevel = value;
			}
		}

		public bool GameRunning{
			get{
				return GameRunning;
			}
			set{
				GameRunning = value;
			}
		}

	    private int _score;
	    public int Lifes { set; get; }
        int IGameStateLevel.Score { get { return _score; } set { _score = value; } }
        int IGameState.Score { get { return _score; } }


	    public void LoseLife(){

		}



    }//end GameState

}//end namespace GameState