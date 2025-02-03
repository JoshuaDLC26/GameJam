using UnityEngine;

public class CollideCheck : MonoBehaviour 
{
	private bool isCollided;
	
	public bool IsCollided
	{
		get { return isCollided; }
	}
	
	void OnCollisionEnter2D(Collision2D collision) 
	{
		if (collision.gameObject.CompareTag("obstacleStage"))
		{
			isCollided = true;
		}
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("obstacleStage"))
		{
			isCollided = false;
		}
	}
	
	public void ResetCollision()
	{
		isCollided = false;
	}
}