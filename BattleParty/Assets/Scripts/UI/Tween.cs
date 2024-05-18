using UnityEngine;
using System.Collections;

public class Tween : MonoBehaviour 
{
	public float multiplier = 1;
	public AnimationCurve forward;
	public AnimationCurve backward;
	
	AnimationCurve current;

	bool doT = false;
	float elapsedTime = 0;

	CanvasGroup group;

	public void Start()
	{
		group = this.GetComponent<CanvasGroup>();
	}

	public void ForwardTween()
	{
		group.blocksRaycasts = true;
		current = forward;
		doT = true;
	}

	public void BackwardTween()
	{
		group.blocksRaycasts = false;
		current = backward;
		doT = true;
	}

	public void Update()
	{
		if(doT)
		{
			if(elapsedTime < 1.05f)
			{
				elapsedTime += Time.unscaledDeltaTime * multiplier;
				group.alpha = current.Evaluate(elapsedTime);
			}
			else
			{
				group.alpha = Mathf.Round(group.alpha);
				elapsedTime = 0;
				doT = false;
			}
		}
	}
}
