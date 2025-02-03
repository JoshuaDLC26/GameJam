using UnityEngine;

public class CollideCheck : MonoBehaviour {
	private bool isCollided;
	public float rayLength = 0.1f; //length of cast
	
	public bool IsCollided {
		get { 
			CheckCollision();
			return isCollided; 
		}
	}
	
	private void CheckCollision() {
		//cast a ray downward from the object's position
		RaycastHit2D hit = Physics2D.Raycast(
			transform.position,
			Vector2.down,
			rayLength,
			LayerMask.GetMask("Ground") //make sure bstacles are on the "Ground" layer
		);

		//update collision state based on raycast result
		isCollided = hit.collider != null && hit.collider.CompareTag("obstacleStage");
	}
	
	public void ResetCollision() {
		isCollided = false;
	}
}