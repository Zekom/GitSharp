﻿/*
 * Copyright (C) 2007, Robin Rosenberg <robin.rosenberg@dewire.com>
 * Copyright (C) 2008, Shawn O. Pearce <spearce@spearce.org>
 * Copyright (C) 2009, Henon <meinrad.recheis@gmail.com>
 *
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or
 * without modification, are permitted provided that the following
 * conditions are met:
 *
 * - Redistributions of source code must retain the above copyright
 *   notice, this list of conditions and the following disclaimer.
 *
 * - Redistributions in binary form must reproduce the above
 *   copyright notice, this list of conditions and the following
 *   disclaimer in the documentation and/or other materials provided
 *   with the distribution.
 *
 * - Neither the name of the Git Development Community nor the
 *   names of its contributors may be used to endorse or promote
 *   products derived from this software without specific prior
 *   written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND
 * CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES,
 * INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
 * OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
 * CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
 * CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT,
 * STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF
 * ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace GitSharp
{
    /**
    * Base class for a set of loaders for different representations of Git objects.
    * New loaders are constructed for every object.
    */
    public abstract class ObjectLoader
    {
        /**
         * @return Git in pack object type, see {@link Constants}.
         */
        public abstract int getType();

        /**
         * @return size of object in bytes
         */
        public abstract long getSize();

        /**
         * Obtain a copy of the bytes of this object.
         * <p>
         * Unlike {@link #getCachedBytes()} this method returns an array that might
         * be modified by the caller.
         * 
         * @return the bytes of this object.
         */
        public byte[] getBytes()
        {
            byte[] data = getCachedBytes();
            byte[] copy = new byte[data.Length];
            Array.Copy(data, 0, copy, 0, data.Length);
            return copy;
        }

        /**
         * Obtain a reference to the (possibly cached) bytes of this object.
         * <p>
         * This method offers direct access to the internal caches, potentially
         * saving on data copies between the internal cache and higher level code.
         * Callers who receive this reference <b>must not</b> modify its contents.
         * Changes (if made) will affect the cache but not the repository itself.
         * 
         * @return the cached bytes of this object. Do not modify it.
         */
        public abstract byte[] getCachedBytes();

        /**
         * @return raw object type from object header, as stored in storage (pack,
         *         loose file). This may be different from {@link #getType()} result
         *         for packs (see {@link Constants}).
         */
        public abstract int getRawType();

        /**
         * @return raw size of object from object header (pack, loose file).
         *         Interpretation of this value depends on {@link #getRawType()}.
         */
        public abstract long getRawSize();
    }

}
