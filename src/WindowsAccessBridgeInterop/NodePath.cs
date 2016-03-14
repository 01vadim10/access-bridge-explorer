// Copyright 2015 Google Inc. All Rights Reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Collections.Generic;
using System.Linq;

namespace AccessBridgeExplorer.WindowsAccessBridge {
  public class NodePath {
    private readonly Stack<AccessibleNode> _nodes = new Stack<AccessibleNode>();

    public int Count {
      get { return _nodes.Count; }
    }

    public AccessibleNode LeafNode {
      get { return _nodes.Last(); }
    }

    public void AddParent(AccessibleNode accessibleNode) {
      _nodes.Push(accessibleNode);
    }

    public AccessibleNode Pop() {
      return _nodes.Pop();
    }
  }
}