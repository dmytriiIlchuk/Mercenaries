extends Area2D

export var speed = 400  # How fast the player will move (pixels/sec).

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
		$AnimatedSprite.play()
	else:
		$AnimatedSprite.stop()
	position += velocity * delta
	position.x = clamp(position.x, 0, screen_size.x)
	position.y = clamp(position.y, 0, screen_size.y)
	if velocity.x != 0:
		$AnimatedSprite.animation = "default"
		$AnimatedSprite.flip_v = false
		# See the note below about boolean assignment
		$AnimatedSprite.flip_h = velocity.x < 0
		if (velocity.x > 0):
			$AnimatedSprite.rotation = PI/2
		else:
			$AnimatedSprite.rotation = -PI/2
	elif velocity.y != 0:
		$AnimatedSprite.animation = "default"
		$AnimatedSprite.flip_v = velocity.y > 0
		$AnimatedSprite.rotation = 0


func _on_Player_body_entered(body):
	emit_signal("hit")
	body.hide()
