﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GitHub.DistributedTask.WebApi;
using GitHub.Runner.Common;
using GitHub.Runner.Common.Util;
using GitHub.Runner.Sdk;
using GitHub.Runner.Worker.Handlers;
using GitHub.Services.WebApi;

namespace GitHub.Runner.Worker.Container
{
    public class DockerHookArgs
    {
        public string ContainerId { get; set; }
    }

    public class DockerHookCommandManager : DockerCommandManager
    {
        public override async Task<int> DockerStart(IExecutionContext context, string containerId)
        {
            // TODO: do we want this flag to come from the .yml as well?
            if (!StringUtil.ConvertToBoolean(Environment.GetEnvironmentVariable("GITHUB_ACTIONS_ALLOW_DOCKER_OVERRIDE")))
            {
                return await base.DockerStart(context, containerId);
            }

            var path = GetDockerHookFileName(nameof(DockerStart));
            var scriptDirectory = Path.GetDirectoryName(path);
            var stepHost = HostContext.CreateService<IDefaultStepHost>();
            var prependPath = string.Join(Path.PathSeparator.ToString(), context.Global.PrependPath.Reverse<string>());
            var dockerHookArgs = new DockerHookArgs { ContainerId = containerId };
            Dictionary<string, string> inputs = new()
            {
                ["standardInInput"] = JsonUtility.ToString(dockerHookArgs),
                ["path"] = path,
                ["shell"] = ScriptHandlerHelpers.GetDefaultShellForScript(path, Trace, prependPath)
            };

            var handlerFactory = HostContext.GetService<IHandlerFactory>();
            var handler = handlerFactory.Create(
                            context,
                            null,
                            stepHost,
                            new ScriptActionExecutionData(),
                            inputs,
                            environment: new Dictionary<string, string>(VarUtil.EnvironmentVariableKeyComparer),
                            context.Global.Variables,
                            actionDirectory: scriptDirectory,
                            localActionContainerSetupSteps: null) as ScriptHandler;
            handler.PrepareExecution(ActionRunStage.Pre); // TODO: find out stage, we only use Start in pre, but double check
            await handler.RunAsync(ActionRunStage.Pre);
            return handler.ExecutionContext.CommandResult == TaskResult.Succeeded ? 0 : 1;
        }

        private string GetDockerHookFileName(string commandName)
        {
            commandName = string.Format("{0}.sh", commandName.ToLower());
            return Path.Combine(HostContext.GetDirectory(WellKnownDirectory.DockerHooks), commandName);
        }
    }
}
