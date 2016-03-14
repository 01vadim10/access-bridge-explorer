﻿// Copyright 2015 Google Inc. All Rights Reserved.
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

using System.Diagnostics;
using System.Windows.Forms;

namespace AccessBridgeExplorer.Model {
  public abstract class NodeModel {
    private static readonly FakeChildNodeModel FakeChildModel = new FakeChildNodeModel();
    private TreeNode _treeNode;

    private class FakeChildNodeModel : NodeModel {
    }

    public TreeNode CreateTreeNode() {
      var treeNode = new TreeNode();
      treeNode.Tag = this;
      SetupTreeNode(treeNode);
      _treeNode = treeNode;
      return treeNode;
    }

    protected void AddFakeChild(TreeNode node) {
      var fakeChild = new TreeNode();
      fakeChild.Tag = FakeChildModel;
      node.Nodes.Add(fakeChild);
    }

    public virtual void SetupTreeNode(TreeNode node) { }

    public void BeforeExpand(object sender, TreeViewCancelEventArgs e) {
      Debug.Assert(ReferenceEquals(_treeNode, e.Node));
      if (_treeNode.Nodes.Count == 1 && _treeNode.Nodes[0].Tag == FakeChildModel) {
        _treeNode.Nodes.Clear();
        AddChildren(e.Node);
      }
    }

    public virtual void AddChildren(TreeNode node) {
    }

    public void ResetChildren(TreeNode treeNode) {
      _treeNode.Nodes.Clear();
      SetupTreeNode(treeNode);
    }
  }
}
