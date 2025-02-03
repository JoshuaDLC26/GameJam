using UnityEngine;

public class CollideCheck : MonoBehaviour {
	private bool isCollided;
	public float rayLength = 0.01f; //length of cast
	public float rayOffset = 0.2f; // Increased slightly for better coverage

    // public LayerMask groundLayer;
	
	public bool IsCollided {
		get { 
			CheckCollision();
			return isCollided; 
		}
	}
	
	private void CheckCollision() {
		Vector2 position = transform.position;
		RaycastHit2D hitCenter = Physics2D.Raycast(position, Vector2.down, rayLength, LayerMask.GetMask("Ground"));
		RaycastHit2D hitLeft = Physics2D.Raycast(position + Vector2.left * rayOffset, Vector2.down, rayLength, LayerMask.GetMask("Ground"));
		RaycastHit2D hitRight = Physics2D.Raycast(position + Vector2.right * rayOffset, Vector2.down, rayLength, LayerMask.GetMask("Ground"));

		isCollided = (hitCenter.collider != null && hitCenter.collider.CompareTag("obstacleStage")) || (hitLeft.collider != null && hitLeft.collider.CompareTag("obstacleStage")) || (hitRight.collider != null && hitRight.collider.CompareTag("obstacleStage"));

		print($"Center hit: {hitCenter.collider}");
        //  Left hit: {hitLeft.collider}, Right hit: {hitRight.collider}");
		print("Collision state: " + isCollided);
		
		// Longer debug rays for better visibility
		// Debug.DrawRay(position, Vector2.down * rayLength, isCollided ? Color.green : Color.red, 0.1f);
		// Debug.DrawRay(position + Vector2.left * rayOffset, Vector2.down * rayLength, isCollided ? Color.green : Color.red, 0.1f);
		// Debug.DrawRay(position + Vector2.right * rayOffset, Vector2.down * rayLength, isCollided ? Color.green : Color.red, 0.1f);
	}
	
	public void ResetCollision() {
		isCollided = false;
	}
}