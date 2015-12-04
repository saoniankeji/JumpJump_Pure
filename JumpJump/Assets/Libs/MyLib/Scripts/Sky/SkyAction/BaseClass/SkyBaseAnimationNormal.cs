﻿using UnityEngine;
using System.Collections;
using DG.Tweening;

public class SkyBaseAnimationNormal : SkyAction
{
	public bool IsPlaying {
		get;
		set;
	}

	public bool Loop {
		get;
		set;
	}

	public bool AutoRun {
		get;
		set;
	}

	public float PlayTime {
		get;
		set;
	}

	public float DelayTime {
		get;
		set;
	}

	public float AutoStartDelayTime {
		get;
		set;
	}

	public SkyAniDuration PositionSkyAniDuration {
		get;
		set;
	}

	public SkyAniCallBack DelayCallBack {
		get;
		set;
	}

	public SkyAniCallBack PlayCallBack {
		get;
		set;
	}

	public SkyBaseSequence  ParentAction {
		get;
		set;
	}

	public virtual  void Init ()
	{
		IsPlaying = false;
		PositionSkyAniDuration = SkyAniDuration.Linear;
		ParentAction = null;
		DelayCallBack = new SkyAniCallBack ();
		DelayCallBack.AddCompleteMethod (() => {
			PlayLoop ();});
		PlayCallBack = new SkyAniCallBack ();
		PlayCallBack.AddCompleteMethod (() => {
			if (PlayCallBack.OnStepCompleteMethod != null) {
				PlayCallBack.OnStepCompleteMethod ();
			}
			PlayNext ();
			if (Loop)
				Delay ();
			else
				IsPlaying = false;
		});
	}
	
	public virtual void PlayLoop ()
	{
		if (PlayCallBack.OnStartMethod != null) {
			PlayCallBack.OnStartMethod ();
		}
	}
	
	public void Play ()
	{
		IsPlaying = true;
		if (DelayTime > 0) {
			Delay ();
		} else {
			PlayLoop ();
		}
	}
	
	public virtual void Delay ()
	{
		DelayTimeAction (DelayTime, DelayCallBack);
	}

	public virtual void PlayNext ()
	{
		if (ParentAction != null) {
			ParentAction.PlayNext (this);
		}
	}
	
	public virtual void RemoveFromParent ()
	{
		if (ParentAction != null) {
			ParentAction.RemoveAction (this);
		}
	}

	private float time;
	private Tweener tw;

	public void DelayTimeAction (float delayTime, SkyAniCallBack skyAnicallBack)
	{
		tw = null;
		tw = runDelayTime (delayTime, delayTime);
		tw.OnComplete (skyAnicallBack.OnCompleteMethod);
	}

	private  Tweener runDelayTime (float endValue, float Duration)
	{
		this.time = 0;
		return DOTween.To (() => this.time, delegate (float x) {
			this.time = x;
		}, endValue, Duration).SetTarget (this).SetEase(Ease.Linear);
	}

	public float GetLeftTime(){
		return PlayTime - time;
	}

	public virtual void Stop(){
		if(tw!=null){
			tw.Complete ();
		}
	}

	public virtual void Pause(){
		if(tw!=null){
			tw.Pause ();
		}
	}
	public virtual void Resume(){
		if(tw!=null){
			tw.Play();
		}
	}

}
