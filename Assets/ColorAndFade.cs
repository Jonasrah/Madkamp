using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorAndFade : MonoBehaviour
{
	public RawImage[] imgs;
	private List<RawImage> allImgs;

	private void Start()
	{
		allImgs = new List<RawImage>();
	}

	public void Splat(Color c)
	{
		for (int i = 0; i < imgs.Length; i++)
		{
			RawImage img = Instantiate(imgs[i], transform);
			img.color = c;
			img.rectTransform.anchoredPosition = new Vector3(Random.Range(-130, 130), Random.Range(-320, 320), 0);
			allImgs.Add(img);
		}
		StartCoroutine(Fade());
	}

	public IEnumerator Fade()
	{
		bool run = true;

		while (run)
		{
			for (int i = 0; i < allImgs.Count; i++)
			{
				allImgs[i].color = Color.Lerp(allImgs[i].color, allImgs[i].color * new Color(1, 1, 1, 0), Time.deltaTime * 2);
				if (allImgs[i].color.a < 0.1f)
				{
					run = false;
				}
			}
			yield return null;
		}

		foreach (var img in allImgs)
		{
			Destroy(img.gameObject);
		}
		allImgs.Clear();
		yield return null;
	}
}
