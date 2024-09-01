using Godot;
using SSEConsumer;
using SSEFun.SSEThings;
using System;
using System.IO;
using HttpClient = System.Net.Http.HttpClient;

namespace NewGameProject;

internal partial class MyRenameTest : Node, IDisposable
{
	public GameState GameState { get; private set; }
	private HttpClient _httpClient = new HttpClient();

	private Stream _stream;
	private StreamReader _streamReader;
	public override void _Ready()
	{
		InitiateSseConnection();
		GD.Print("SSE INitialized");
	}

	private void InitiateSseConnection()
	{
		_stream = _httpClient.GetStreamAsync($"http://localhost:5003/WeatherForecast/SSE?userId={Guid.NewGuid()}").Result;
		_streamReader = new StreamReader(_stream);
	}

	public override void _Process(double delta)
	{
		var rawData = _streamReader.ReadLine();
		if (string.IsNullOrEmpty(rawData)) return;



		var deserialized = SSEMessaging.ParseRawMessage<GameState>(rawData);

		this.GameState = deserialized;

		foreach (var item in deserialized.Insects)
		{
			GD.Print($"Id: {item.Id} {item.X}X, {item.Y}Y");
		}
	}

	~MyRenameTest()
	{
		DisposeOf();
	}

	private void DisposeOf()
	{
		try
		{
			_stream.Close();
			_stream.Dispose();
			_stream.Close();
			_stream.Dispose();
		}
		catch (Exception ex)
		{
			GD.Print("An element was already diposed of");
		}
	}
}
