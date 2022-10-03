using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames.BasicApi.Events;

public class PlayStoreLeaderBoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
		PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder().Build());
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesPlatform.Activate();

    }

    public void doLogin()
	{
		if (!Social.localUser.authenticated)
		{
			PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptAlways, (success) =>
			{
				DoShowBoad();
			});
		}
		else
			DoShowBoad();
	}
	public void doLogout()
	{
		((PlayGamesPlatform)Social.Active).SignOut();
	}

	public void DoShowLeaderBoard()
	{
		doLogin();
	}

	public void DoShowBoad()
	{
		Social.ReportScore(PlayerPrefs.GetInt("BestScore"), GPGSIds.leaderboard_ranking, (bool bSuccess) =>
		{
			if (bSuccess)
			{
				Debug.Log("ReportLeaderBoard Success");
			}
			else
			{
				Debug.Log("ReportLeaderBoard Fall");
			}
		}
);
		Social.ShowLeaderboardUI();
	}
}
