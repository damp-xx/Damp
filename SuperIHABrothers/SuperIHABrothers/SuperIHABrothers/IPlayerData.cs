///////////////////////////////////////////////////////////
//  IPlayerData.cs
//  Implementation of the Interface IPlayerData
//  Generated by Enterprise Architect
//  Created on:      09-maj-2013 09:35:36
//  Original author: Filip
///////////////////////////////////////////////////////////




public interface IPlayerData  {

	/// 
	/// <param name="highscore"></param>
    int Highscore { set; }

    bool GameRunning { get; set; }

	/// 
	/// <param name="name"></param>
	void SetPlayerName(string name);
}//end IPlayerData