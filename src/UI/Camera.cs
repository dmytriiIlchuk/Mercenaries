using Godot;

public class Camera : Camera2D
{
    public float PanSpeed = 30.0f;
    public float Speed = 30.0f;
    public float ZoomMargin = 0.1f;
    public float ZoomSpeed = 20.0f;
    public float ZoomMin = 0.25f;
    public float ZoomMax = 3.0f;
    public float ZoomFactor = 1.0f;
    public float MarginX = 200.0f;
    public float MarginY = 100.0f;
    public bool Zooming = false;
    public bool IsDragging = false;

    public Vector2 MousePosition = new Vector2();
    public Vector2 MousePositionGlobal = new Vector2();
    public Vector2 Start = new Vector2();
    public Vector2 End = new Vector2();
    public Vector2 StartV = new Vector2();
    public Vector2 EndV = new Vector2();
    public Vector2 ZoomPos = new Vector2();
    public Vector2 MoveToPoint = new Vector2();

    public override void _Ready()
    {
    }

    public override void _Process(float delta)
    {
        var inpx = (Input.IsActionPressed("ui_right") ? 1 : 0) - (Input.IsActionPressed("ui_left") ? 1 : 0);

        var inpy = (Input.IsActionPressed("ui_down") ? 1 : 0) - (Input.IsActionPressed("ui_up") ? 1 : 0);

        this.Position = new Vector2(
            Mathf.Lerp(this.Position.x, this.Position.x + inpx * Speed * this.Zoom.x, Speed * delta),
            Mathf.Lerp(this.Position.y, this.Position.y + inpy * Speed * this.Zoom.y, Speed * delta)
        );

        if (Input.IsKeyPressed((int)KeyList.Control))
        {
            float x = Position.x;
            float y = Position.y;
            if (MousePosition.x < MarginX)
            {
                x = Mathf.Lerp(this.Position.x, this.Position.x - Mathf.Abs(MousePosition.x - MarginX) / MarginX * PanSpeed * this.Zoom.x, PanSpeed * delta);
            }
            else if (MousePosition.x > OS.WindowSize.x - MarginX)
            {
                x = Mathf.Lerp(this.Position.x, this.Position.x + Mathf.Abs(MousePosition.x - OS.WindowSize.x + MarginX) / MarginX * PanSpeed * Zoom.x, PanSpeed * delta);
            }

            if (MousePosition.y < MarginY)
            {
                y = Mathf.Lerp(this.Position.y, this.Position.y - Mathf.Abs(MousePosition.y - MarginY) / MarginY * PanSpeed * this.Zoom.y, PanSpeed * delta);
            }
            else if (MousePosition.y > OS.WindowSize.y - MarginY)
            {
                y = Mathf.Lerp(this.Position.y, this.Position.y + Mathf.Abs(MousePosition.y - OS.WindowSize.y + MarginY) / MarginY * PanSpeed * this.Zoom.y, PanSpeed * delta);
            }

            this.Position = new Vector2(x, y);
        }

        this.Zoom = new Vector2(
        Mathf.Lerp(this.Zoom.x, this.Zoom.x * ZoomFactor, ZoomSpeed * delta),
        Mathf.Lerp(this.Zoom.y, this.Zoom.y * ZoomFactor, ZoomSpeed * delta));


        this.Zoom = new Vector2(
            Mathf.Clamp(this.Zoom.x, ZoomMin, ZoomMax),
            Mathf.Clamp(this.Zoom.y, ZoomMin, ZoomMax));

        if (!Zooming)
        {
            ZoomFactor = 1.0f;
        }
    }

    public override void _Input(InputEvent @event)
    {
        if (Mathf.Abs(ZoomPos.x - GetGlobalMousePosition().x) > ZoomMargin)
        {
            this.ZoomFactor = 1.0f;
        }

        if (Mathf.Abs(ZoomPos.y - GetGlobalMousePosition().y) > ZoomMargin)
        {
            this.ZoomFactor = 1.0f;
        }

        if (@event is InputEventMouseButton)
        {
            InputEventMouseButton inputEventMouseButton = (InputEventMouseButton)@event;
            if (inputEventMouseButton.IsPressed())
            {
                this.Zooming = true;
                switch (inputEventMouseButton.ButtonIndex)
                {
                    case (int)ButtonList.WheelUp:
                        ZoomFactor -= 0.01f * ZoomSpeed;
                        ZoomPos = GetGlobalMousePosition();
                        break;
                    case (int)ButtonList.WheelDown:
                        ZoomFactor += 0.01f * ZoomSpeed;
                        ZoomPos = GetGlobalMousePosition();
                        break;
                };
            }
            else
            {
                Zooming = false;
            }
        }

        if (@event is InputEventMouseMotion)
        {
            MousePosition = ((InputEventMouseMotion)@event).Position;
            MousePositionGlobal = GetGlobalMousePosition();
        }
    }
}