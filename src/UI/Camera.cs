using Godot;

public class Camera : Camera2D
{
    private static CameraConfig cameraConfig = Config.cameraConfig;
    private float PanSpeed = cameraConfig.PanSpeed;
    private float Speed = cameraConfig.Speed;
    private float ZoomMargin = cameraConfig.ZoomMargin;
    private float ZoomSpeed = cameraConfig.ZoomSpeed;
    private float ZoomMin = cameraConfig.ZoomMin;
    private float ZoomMax = cameraConfig.ZoomMax;
    private float MarginX = cameraConfig.MarginX;
    private float MarginY = cameraConfig.MarginY;

    private float ZoomFactor = 1.0f;
    private bool Zooming = false;
    private bool IsDragging = false;

    private Vector2 MousePosition = new Vector2();
    private Vector2 MousePositionGlobal = new Vector2();
    private Vector2 Start = new Vector2();
    private Vector2 End = new Vector2();
    private Vector2 StartV = new Vector2();
    private Vector2 EndV = new Vector2();
    private Vector2 ZoomPos = new Vector2();
    private Vector2 MoveToPoint = new Vector2();

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