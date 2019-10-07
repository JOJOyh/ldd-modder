﻿using LDDModder.LDD.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDDModder.LifExtractor.Models
{
    class LifFolderInfo : LifItemInfo<LifFile.FolderEntry>
    {
        public LifFile.FolderEntry Folder => Entry as LifFile.FolderEntry;

        public override string Name => Folder.IsRootDirectory ? LifName : base.Name;

        public LifFolderInfo(LifFile.FolderEntry folder) : base(folder)
        {
            ItemImageKey = "folder";
        }
    }
}
