extends Node2D

export var speed = 400  # How fast the player will move (pixels/sec).

var played = false
var screen_size  # Size of the game window.
signal hit

func _ready():
	screen_size = get_viewport_rect().size

func _process(delta):
	var velocity = Vector2()  # The player's movement vector.
	if Input.is_action_pressed("ui_right"):
		velocity.x += 1
	if Input.is_action_pressed("ui_left"):
		velocity.x -= 1
	if Input.is_action_pressed("ui_down"):
		velocity.y += 1
	if Input.is_action_pressed("ui_up"):
		velocity.y -= 1
	if velocity.length() > 0:
		velocity = velocity.normalized() * speed
	position += velocity * delta
	position.x = clamp(position.x, 0, screen_size.x)
	position.y = clamp(position.y, 0, screen_size.y)
	if !played:
		$AnimationPlayer.play("Attack")
		played = true
	


func _on_Unit_body_entered(body):
	emit_signal("hit")
	body.hide()
	$AnimationPlayer.play_backwards("Attack")
