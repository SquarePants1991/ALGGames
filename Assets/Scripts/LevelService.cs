using UnityEngine;
using System.Collections;

public enum BufEffectType {
	MultiScore,
	ContinuousShoot,
	LargeBullet
}

public class BufEffect {
	public BufEffectType effectType;
	public float effectFactor;
	public float leftTime; // s

	public BufEffect(BufEffectType effectType, float effectFactor, float leftTime) {
		this.effectType = effectType;
		this.effectFactor = effectFactor;
		this.leftTime = leftTime;
	}
}

public class LevelService
{
	public static LevelService sharedService = new LevelService();
	public float totalScore = 0;
	public float healthPoint {
		set { 
			if (value <= 0) {
				_healthPoint = 0;
			} else {
				_healthPoint = value;
			}
		}
		get { 
			return _healthPoint;
		}
	}
	public ArrayList bufs;

	private float _healthPoint;

	public LevelService() {
		bufs = new ArrayList ();
	}

	public void AddBuf(BufEffect bufEffect) {
		bool bufExists = false;
		foreach (BufEffect buf in bufs) {
			if (buf.effectType == bufEffect.effectType) {
				buf.effectFactor = buf.effectFactor < bufEffect.effectFactor ? bufEffect.effectFactor : buf.effectFactor;
				buf.leftTime = bufEffect.leftTime;
				bufExists = true;
			}
		}
		if (bufExists == false) {
			bufs.Add (bufEffect);
		}
	}

	public BufEffect GetBuf(BufEffectType bufType) {
		foreach (BufEffect buf in bufs) {
			if (buf.effectType == bufType) {
				return buf;
			}
		}
		return null;
	}

	public void Reset() {
		totalScore = 0;
		healthPoint = 100;
		bufs = new ArrayList ();
		AddBuf (new BufEffect(BufEffectType.ContinuousShoot, 5, 10));
	}

}

