using System;
using System.Windows.Forms;

struct Point
{
    public float x;
    public float y;
}

class PanZoom
{
    float offsetX = 0f;
    float offsetY = 0f;
    float scale = 1f;

    float minZoom = 0.1f;
    float maxZoom = 10f;

    public PanZoom()
    {

    }

    public PanZoom(float offsetX, float offsetY, float scale, float minZoom, float maxZoom)
    {
        this.OffsetX = offsetX;
        this.OffsetY = offsetY;
        this.Scale = scale;
        this.minZoom = minZoom;
        this.maxZoom = maxZoom;
    }

    public float OffsetX { get => offsetX; set => offsetX = value; }
    public float OffsetY { get => offsetY; set => offsetY = value; }
    public float Scale
    {
        get => scale;
        set => scale = Math.Min(Math.Max(minZoom, value), maxZoom);
    }

    public float MinZoom
    {
        get => minZoom;
        set
        {
            if (minZoom <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(minZoom));
            }

            minZoom = value;
        }
    }

    public float MaxZoom
    {
        get => maxZoom;
        set
        {
            if (maxZoom <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxZoom));
            }

            maxZoom = value;
        }
    }

    public bool Click
    {
        get => click;
        set => click = value;
    }

    // ======================================================================= //

    // World to screen functions:
    public float WorldToScreenX(float worldX) { return (worldX - OffsetX) * Scale; }
    public float WorldToScreenY(float worldY) { return (worldY - OffsetY) * Scale; }

    // Screen to world functions:
    public float ScreenToWorldX(float screenX) { return (screenX / Scale) + OffsetX; }
    public float ScreenToWorldY(float screenY) { return (screenY / Scale) + OffsetY; }

    // ======================================================================= //

    // Mouse functions:
    bool drag = false;
    Point dragStart;
    Point dragEnd;

    bool click = true;

    public void MouseDown(float mouseX, float mouseY)
    {
        dragStart.x = mouseX;
        dragStart.y = mouseY;

        drag = true;
    }

    public void MouseMove(float mouseX, float mouseY)
    {
        if (drag)
        {
            if (click) click = false;

            // Gets drag end:
            dragEnd.x = mouseX;
            dragEnd.y = mouseY;

            // Updates the offset:
            OffsetX -= (dragEnd.x - dragStart.x) / Scale;
            OffsetY -= (dragEnd.y - dragStart.y) / Scale;

            dragStart = dragEnd; // Resets the dragStart.
        }
    }

    public void MouseUp() { drag = false; }

    public void MouseWheel(float mouseX, float mouseY, float delta)
    {
        float mouseBeforeZoomX = ScreenToWorldX(mouseX);
        float mouseBeforeZoomY = ScreenToWorldY(mouseY);


        // Zooms in or out:
        Scale += delta * (-0.001f) * (Scale / 2);

        // Restrict zoom:
        Scale = Math.Min(Math.Max(MinZoom, Scale), MaxZoom);


        // Mouse after zoom:
        float mouseAfterZoomX = ScreenToWorldX(mouseX);
        float mouseAfterZoomY = ScreenToWorldY(mouseY);


        // Adjusts offset so the zoom occurs relative to the mouse position:
        OffsetX += (mouseBeforeZoomX - mouseAfterZoomX);
        OffsetY += (mouseBeforeZoomY - mouseAfterZoomY);
    }
}