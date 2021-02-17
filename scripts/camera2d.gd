extends Camera2D


export var pan_speed = 10.0
export var speed = 10.0
export var zoommargin = 0.1
export var zoomspeed = 10.0

export var zoomMin = 0.25
export var zoomMax = 3.0
export var margin_x = 200.0
export var margin_y = 100.0

var mouse_position = Vector2()
var mouse_position_global = Vector2()
var start = Vector2()
var end = Vector2()
var startv = Vector2()
var endv = Vector2()
var zoompos = Vector2()
var zoomfactor = 1.0
var zooming = false
var is_dragging = false
var move_to_point = Vector2()

func _ready():
	pass # Replace with function body.



func _process(delta):
	var inpx = int(Input.is_action_pressed("ui_right")) - int(Input.is_action_pressed("ui_left"))
	var inpy = int(Input.is_action_pressed("ui_down")) - int(Input.is_action_pressed("ui_up"))
	
	position.x = lerp(position.x, position.x + inpx * speed * zoom.x, speed * delta)
	position.y = lerp(position.y, position.y + inpy * speed * zoom.y, speed * delta)
	
	if Input.is_key_pressed(KEY_CONTROL):
		if mouse_position.x < margin_x:
			position.x = lerp(position.x, position.x - abs(mouse_position.x - margin_x)/margin_x * pan_speed * zoom.x, pan_speed * delta)
		elif mouse_position.x > OS.window_size.x - margin_x:
			position.x = lerp(position.x, position.x + abs(mouse_position.x - OS.window_size.x + margin_x)/margin_x * pan_speed * zoom.x, pan_speed * delta)
		if mouse_position.y < margin_y:
			position.y = lerp(position.y, position.y - abs(mouse_position.y - margin_y)/margin_y * pan_speed * zoom.y, pan_speed * delta)
		elif mouse_position.y > OS.window_size.y - margin_y:
			position.y = lerp(position.y, position.y + abs(mouse_position.y - OS.window_size.y + margin_y)/margin_y * pan_speed * zoom.y, pan_speed * delta)
	zoom.x = lerp(zoom.x, zoom.x * zoomfactor, zoomspeed * delta)
	zoom.y = lerp(zoom.y, zoom.y * zoomfactor, zoomspeed * delta)
	zoom.x = clamp(zoom.x, zoomMin, zoomMax)
	zoom.y = clamp(zoom.y, zoomMin, zoomMax)

func _input(event):
	if abs(zoompos.x - get_global_mouse_position().x) > zoommargin:
		zoomfactor = 1.0
	if abs(zoompos.y - get_global_mouse_position().y) > zoommargin:
		zoomfactor = 1.0
	if event is InputEventMouseButton:
		if event.is_pressed():
			zooming = true
			if event.button_index == BUTTON_WHEEL_UP:
				zoomfactor -= 0.01 * zoomspeed
				zoompos = get_global_mouse_position()
			if event.button_index == BUTTON_WHEEL_DOWN:
				zoomfactor += 0.01 * zoomspeed
				zoompos = get_global_mouse_position()
		else:
			zooming = false
	if event is InputEventMouseMotion:
		mouse_position = event.position
		mouse_position_global = get_global_mouse_position()
