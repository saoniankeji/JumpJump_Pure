﻿using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class PlayPanel : UIWindow {
  public TextMeshProUGUI scoreText;
	public TextMeshProUGUI roleStateText;
  
	public Button slowButton;
	public Text slowText;

	public Button fastButton;
	public Text fastText;

	DelayAction count;

	public override void Init ()
	{
		base.Init ();
		count = new DelayAction (10f,startAction,actionActive);
	}

  public void SetScore(string score){
		scoreText.text = score;
	}

	public void OnPause(){
		GameController.GetInstance ().GetPausePanel().ShowIn ();
		GameController.GetInstance ().GetPlayGameInstance().OnPause ();
		Pause ();
	}

	public void OnTouchDown(){
		GameController.GetInstance ().GetPlayGameInstance().OnTouchDown ();
	}

	void Update () {
		SetScore (GameData.Instance().M_RunningData.M_Score + "");
		roleStateText.text = GameData.Instance ().M_RunningData.M_RoleState; 
		if (count.IsPlaying) {
			slowText.text = ((int)count.GetLeftTime() +1 )+"";
			fastText.text = ((int)count.GetLeftTime() +1 )+"";
		} else {
			slowText.text = "SLOW";
			fastText.text = "FAST";
		}

	}

	void startAction(){
		slowButton.interactable = false;
		fastButton.interactable = false;
	}

	void actionActive(){
		slowButton.interactable = true;
		fastButton.interactable = true;
	}

	public void SlowAction(){
		count.Play ();
		GameController.GetInstance ().GetPlayGameInstance ().OnSkill_SlownDown ();
	}

	public void FastAction(){
		count.Play ();
		GameController.GetInstance ().GetPlayGameInstance ().OnSkill_SpeedUp ();
	}

	public void Start(){
		count.Stop ();
	}

	public void Pause(){
		count.Pause ();
	}

	public void Resume(){
		count.Resume ();
	}

}
