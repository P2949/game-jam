using Godot;
using System;
using System.Dynamic;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

public abstract partial class EnemyBehavior : CharacterBody2D {
	
	public virtual float jumpVelocity {get; set;}
	public virtual int speed {get; set;}
	public virtual int fallAcceleration {get; set;}
	
	public virtual int health {get; set;}
	public virtual int maxHealth {get; set;}
	
	public virtual float touchDamage {get; set;}
	
	
	Vector2 _targetVelocity = Vector2.Zero;
	bool jump = false;
	float moveXDir = 0;
	
	public async Task MoveX(float relativeX) {
		moveXDir = relativeX;
		
		Func<bool> waitUntilCondition = relativeX <= 0 ? new Func<bool>(() => { return moveXDir >= 0; }) : new Func<bool>(() => { return moveXDir <= 0; });
		
		await AsyncUtils.WaitUntil(condition: waitUntilCondition, checkFreqMS: 40);
	}
	
	public void Jump() {
		jump = true;
	}
	
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 direction = Vector2.Zero;
		
		if (jump && IsOnFloor())
		{
			_targetVelocity.Y = jumpVelocity;
			jump = false;
		}

		if (moveXDir > 0)
		{
			direction.X += 1.0f;
			moveXDir -= 1.0f * speed;
		}
		else if (moveXDir < 0)
		{
			direction.X -= 1.0f;
			moveXDir += 1.0f * speed;
		}

		if (direction != Vector2.Zero)
		{
			direction = direction.Normalized();
		}

		// Ground velocity
		_targetVelocity.X = direction.X * speed;
		
		
		
		// Vertical velocity
		if (!IsOnFloor()) // If in the air, fall towards the floor. Literally gravity
		{
			_targetVelocity.Y += fallAcceleration * (float)delta;
		}
		

		// Moving the character
		Velocity = _targetVelocity;
		MoveAndSlide();
		
		if (IsOnFloor() || IsOnCeiling()) {
			_targetVelocity.Y = 0;
		}
	}
	
}
