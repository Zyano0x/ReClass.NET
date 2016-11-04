using System;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Resources;
using ReClassNET.Forms;
using ReClassNET.Logger;
using ReClassNET.Nodes;
using ReClassNET.DataExchange;
using ReClassNET.Util;
using ReClassNET.CodeGenerator;

namespace ReClassNET.Plugins
{
	internal sealed class DefaultPluginHost : IPluginHost
	{
		public MainForm MainWindow { get; }

		public ResourceManager Resources => Properties.Resources.ResourceManager;

		public RemoteProcess Process { get; }

		public ILogger Logger { get; }

		public Settings Settings => Program.Settings;

		public DefaultPluginHost(MainForm form, RemoteProcess process, ILogger logger)
		{
			Contract.Requires(form != null);
			Contract.Requires(process != null);
			Contract.Requires(logger != null);

			MainWindow = form;
			Process = process;
			Logger = logger;
		}

		public void RegisterNodeInfoReader(INodeInfoReader reader)
		{
			BaseNode.NodeInfoReader.Add(reader);
		}

		public void DeregisterNodeInfoReader(INodeInfoReader reader)
		{
			BaseNode.NodeInfoReader.Remove(reader);
		}

		public void RegisterNodeType(Type type, string name, Image icon, ICustomSchemaConverter converter, ICustomCodeGenerator generator)
		{
			CustomSchemaConvert.RegisterCustomType(converter);
			CustomCodeGenerator.RegisterCustomType(generator);

			MainWindow.RegisterNodeType(type, name, icon ?? Properties.Resources.B16x16_Plugin);
		}

		public void DeregisterNodeType(Type type, ICustomSchemaConverter converter, ICustomCodeGenerator generator)
		{
			CustomSchemaConvert.DeregisterCustomType(converter);
			CustomCodeGenerator.DeregisterCustomType(generator);

			MainWindow.DeregisterNodeType(type);
		}
	}
}
