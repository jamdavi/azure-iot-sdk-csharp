// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

#if NET451
using System.Net.Http.Formatting;
#endif

namespace Microsoft.Azure.Devices.Shared
{
    internal static class HttpMessageHelper
    {
#if !NET451
        private const string ApplicationJson = "application/json";
#endif

#if NET451
        private static readonly JsonMediaTypeFormatter s_jsonFormatter = new JsonMediaTypeFormatter();
#endif

        internal static void SetHttpRequestMessageContent<T>(HttpRequestMessage requestMessage, T entity)
        {
#if NET451
            requestMessage.Content = new ObjectContent<T>(entity, s_jsonFormatter);
#else
            string str = JsonConvert.SerializeObject(entity);
            requestMessage.Content = new StringContent(str, Encoding.UTF8, ApplicationJson);
#endif
        }

        internal static async Task<T> ReadHttpResponseMessageContentAsync<T>(HttpResponseMessage message, CancellationToken token)
        {
#if NET451
            T entity = await message.Content.ReadAsAsync<T>(token).ConfigureAwait(false);
#elif NET5_0
            string str = await message.Content.ReadAsStringAsync(token).ConfigureAwait(false);
            T entity = JsonConvert.DeserializeObject<T>(str);
#else
            _ = token;
            string str = await message.Content.ReadAsStringAsync().ConfigureAwait(false);
            T entity = JsonConvert.DeserializeObject<T>(str);
#endif
            return entity;
        }
    }
}