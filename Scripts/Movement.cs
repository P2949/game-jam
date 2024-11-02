using Godot;
using System;

public partial class Movement : CharacterBody2D
{
	
	public const float JumpVelocity = -400.0f;
	
	// player speed
	[Export] public int Speed { get; set; } = 500;
	
	// The downward acceleration when in the air, in meters per second squared.
	[Export] public int FallAcceleration { get; set; } = 200;

	private Vector2 _targetVelocity = Vector2.Zero;

	public async override void _PhysicsProcess(double delta)
	{
		var direction = Vector2.Zero;

		if (Input.IsActionPressed("jump") && IsOnFloor())
		{
			_targetVelocity.Y += JumpVelocity;
		} 
		if (Input.IsActionPressed("move_right"))
		{
			direction.X += 1.0f;
		}
		if (Input.IsActionPressed("move_left"))
		{
			direction.X -= 1.0f;
		}
		if (direction != Vector2.Zero)
		{
			direction = direction.Normalized();
		}

		// Ground velocity (best described as X axis velocity)
		_targetVelocity.X = direction.X * Speed;
		
		
		
		// Vertical velocity
		if (!IsOnFloor()) // If in the air, fall towards the floor. Literally gravity
		{
			_targetVelocity.Y += FallAcceleration * (float)delta;
		}
		

		// Moving the character
		Velocity = _targetVelocity;
		MoveAndSlide();
		
		if (IsOnFloor() || IsOnCeiling()) { // leo doing god's work
			_targetVelocity.Y = 0;
		}
		
	}
}
