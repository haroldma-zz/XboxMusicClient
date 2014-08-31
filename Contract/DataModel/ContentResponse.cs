// Copyright (c) Microsoft Corporation
// All rights reserved. 
//
// Licensed under the Apache License, Version 2.0 (the ""License""); you may
// not use this file except in compliance with the License. You may obtain a
// copy of the License at http://www.apache.org/licenses/LICENSE-2.0 
//
// THIS CODE IS PROVIDED *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS OF
// ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY
// IMPLIED WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE,
// MERCHANTABLITY OR NON-INFRINGEMENT. 
//
// See the Apache Version 2.0 License for specific language governing
// permissions and limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Microsoft.Xbox.Music.Platform.Contract.DataModel
{
    [DataContract(Namespace = Constants.Xmlns)]
    public class ContentResponse : BaseResponse
    {
        [DataMember(EmitDefaultValue = false)]
        public XboxPaginatedList<XboxArtist> Artists { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public XboxPaginatedList<XboxAlbum> Albums { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public XboxPaginatedList<XboxTrack> Tracks { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public XboxPaginatedList<XboxPlaylist> Playlists { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public XboxPaginatedList<ContentItem> Results { get; set; } 

        [DataMember(EmitDefaultValue = false)]
        public GenreList Genres { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public string Culture { get; set; }

        private void GenericAddPieceOfContent<T>(Content content, XboxPaginatedList<T> container, Func<XboxPaginatedList<T>> containerInstanciator)
            where T : Content
        {
            if (container == null)
            {
                container = containerInstanciator();
            }
            if (container.Items == null)
            {
                container.Items = new List<T>();
            }
            container.Items.Add(content as T);
        }

        public void AddPieceOfContent(Content content)
        {
            if (content is XboxArtist)
            {
                GenericAddPieceOfContent(content, Artists, () => Artists = new XboxPaginatedList<XboxArtist>());
            }
            else if (content is XboxAlbum)
            {
                GenericAddPieceOfContent(content, Albums, () => Albums = new XboxPaginatedList<XboxAlbum>());
            }
            else if (content is XboxTrack)
            {
                GenericAddPieceOfContent(content, Tracks, () => Tracks = new XboxPaginatedList<XboxTrack>());
            }
            else if (content is XboxPlaylist)
            {
                GenericAddPieceOfContent(content, Playlists, () => Playlists = new XboxPaginatedList<XboxPlaylist>());
            }
            else
            {
                throw new ArgumentException("Unknown content type:" + content.GetType().ToString());
            }
        }

        public IEnumerable<Content> GetAllTopLevelContent()
        {
            return GetAllContentLists()
                .Where(c => c != null && c.ReadOnlyItems != null)
                .SelectMany(x => x.ReadOnlyItems);
        }

        public IEnumerable<IXboxPaginatedList<Content>> GetAllContentLists()
        {
            yield return Artists;
            yield return Albums;
            yield return Tracks;
            yield return Playlists;
        }
    }
}
