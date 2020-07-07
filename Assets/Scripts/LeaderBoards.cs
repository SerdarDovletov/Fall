using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using UnityEngine.Rendering;
public class LeaderBoards : MonoBehaviour
{
    private ILeaderboard leaderboard;
    private string leaderboard_id = "turkmengamesfallleaderboard";
    void Start(){
        if(Social.localUser.authenticated) Social.ReportScore(GameController.instance.bestScore, leaderboard_id, success => {});
    }

    public void OpenLeaderboard(){
        if (GameController.instance.audioOn) GameController.instance.Select.Play();
        Social.localUser.Authenticate(success => { });
        leaderboard = Social.CreateLeaderboard();
        leaderboard.id = leaderboard_id;
        if (Social.localUser.authenticated) Social.ReportScore(GameController.instance.bestScore, leaderboard_id, success => { });
        Social.ShowLeaderboardUI();
    }
}