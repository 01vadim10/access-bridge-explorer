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

using System;

namespace AccessBridgeExplorer.WindowsAccessBridge {
  /// <summary>
  /// Single entry point to access all events exposed by the Java Access Bridge
  /// DLL (<see cref="AccessBridge.Events"/>).
  /// </summary>
  public partial class AccessBridgeEvents : IDisposable {
    private readonly AccessBridgeEventsNative _nativeEvents;

    public AccessBridgeEvents(AccessBridgeLibraryFunctions libraryFunctions) {
      _nativeEvents = new AccessBridgeEventsNative(libraryFunctions);
    }

    public AccessBridgeEventsNative NativeEvents {
      get { return _nativeEvents; }
    }

    public void Dispose() {
      DetachForwarders();
    }

    private JavaObjectHandle Wrap(int vmid, JOBJECT64 handle) {
      return new JavaObjectHandle(vmid, handle);
    }
  }
}
