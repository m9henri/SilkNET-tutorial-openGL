using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.Windowing;
using Silk.NET.OpenGL;
using System.Drawing;

// https://dotnet.github.io/Silk.NET/docs/opengl/c1/2-hello-quad#element-buffer-objects-ebos

namespace SilkNET_tutorial;

class Program
{
    private static IWindow _window;
    private static GL _gl;
    private static uint _vao;
    private static uint _vbo;

    static void Main(string[] args)
    {
        WindowOptions options = WindowOptions.Default;
        options.Size = new Vector2D<int>(800, 600);

        _window = Window.Create(options);

        _window.Load += OnLoad;
        _window.Update += OnUpdate;
        _window.Render += OnRender;

        _window.Run();
    }

    private static unsafe void OnLoad()
    {
        _window.Center();

        IInputContext input = _window.CreateInput();
        for (int i = 0; i < input.Keyboards.Count; i++)
        {
            input.Keyboards[i].KeyDown += KeyDown;
        }

        _gl = _window.CreateOpenGL();

        _gl.ClearColor(Color.CornflowerBlue);
        _gl.Clear(ClearBufferMask.ColorBufferBit);

        _vao = _gl.GenVertexArray();
        _gl.BindVertexArray(_vao);

        float[] vertices =
        {
             0.5f,  0.5f,  0.0f,
             0.5f, -0.5f,  0.0f,
            -0.5f, -0.5f,  0.0f,
            -0.5f,  0.5f,  0.0f
        };

        _vbo = _gl.GenBuffer();
        _gl.BindBuffer(BufferTargetARB.ArrayBuffer, _vbo);

        fixed (float* buf = vertices) _gl.BufferData(BufferTargetARB.ArrayBuffer, (nuint)(vertices.Length * sizeof(float)), buf, BufferUsageARB.StaticDraw);
    }

    private static void OnUpdate(double deltaTime) { }

    private static unsafe void OnRender(double deltaTime) { }

    private static void KeyDown(IKeyboard keyboard, Key key, int keyCode)
    {
        if (key == Key.Escape) { _window.Close(); }
    }
}
