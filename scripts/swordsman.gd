extends Node2D

export var speed = 400  # How fast the player will move (pixels/sec).

var played = false
var velocity = Vector2(0, 0)
var screen_size  # Size of the game window.
signal hit

func _ready():
	screen_size = get_viewport_rect().size

func _process(delta: float):
	move(delta)

func attack():
	$AnimationPlayer.play("Attack")

func shield_up():
	$AnimationPlayer.play("ShieldUp")

func move(delta: float):
	if velocity.length() > 0:
		velocity = velocity.normalized() * speed
	position += velocity * delta
	position.x = clamp(position.x, 0, screen_size.x)
	position.y = clamp(position.y, 0, screen_size.y)
