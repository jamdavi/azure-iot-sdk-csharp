// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

namespace Microsoft.Azure.Devices.Shared
{
    internal sealed class TaskCompletionSource : TaskCompletionSource<int>
    {
        public static readonly TaskCompletionSource Void = CreateVoidTcs();

        public TaskCompletionSource(object state)
            : base(state)
        {
        }

        public TaskCompletionSource()
        {
        }

        public bool TrySetResult() => TrySetResult(0);

        public void SetResult() => SetResult(0);

        public override string ToString() => "TaskCompletionSource[status: " + Task.Status.ToString() + "]";

        static TaskCompletionSource CreateVoidTcs()
        {
            var tcs = new TaskCompletionSource();
            tcs.TrySetResult();
            return tcs;
        }
    }
}