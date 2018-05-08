﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocSystEntities.StyleStructure;

namespace DocSystBusinessLogicInterface.StyleStructureBusinessLogicInterface
{
    public interface IFormatBusinessLogic
    {
        void Add(Format format);
        void Delete(Guid id);
        void Modify(Format format);
        IList<Format> Get();
        void AddStyle(Guid id, StyleClass styleClass);
        void RemoveStyle(Guid id1, Guid id2);
        void Get(Guid id);
    }
}