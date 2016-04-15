﻿// Copyright 2016 Google Inc. All Rights Reserved.
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

using System.Windows.Forms;

namespace AccessBridgeExplorer.Utils {
  /// <summary>
  /// Implementation of <see cref="TreeListViewModel{TNode}"/> using a single
  /// <see cref="TreeNode"/> as the input.
  /// </summary>
  public class TreeNodeViewModel : TreeListViewModel<TreeNode> {
    private readonly TreeNode _rootNode;
    private readonly bool _isRootVisible;

    public TreeNodeViewModel(TreeNode rootNode, bool isRootVisible) {
      _rootNode = rootNode;
      _isRootVisible = isRootVisible;
    }

    public override bool IsRootVisible() {
      return _isRootVisible;
    }

    public override TreeNode GetRootNode() {
      return _rootNode;
    }

    public override bool IsNodeExpandable(TreeNode modelNode) {
      return modelNode.Nodes.Count > 0;
    }

    public override bool IsNodeExpanded(TreeNode modelNode) {
      return modelNode.IsExpanded;
    }

    public override int GetChildrenCount(TreeNode modelNode) {
      return modelNode.Nodes.Count;
    }

    public override TreeNode GetChildAt(TreeNode modelNode, int index) {
      return modelNode.Nodes[index];
    }

    public override string GetNodeText(TreeNode modelNode) {
      return modelNode.Text;
    }
  }
}