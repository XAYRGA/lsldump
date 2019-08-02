using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lsldump
{
    public enum SLAssetType
    {
        AT_TEXTURE = 0,
        // Used for painting the faces of geometry.
        // Stored in typical j2c stream format.

        AT_SOUND = 1,
        // Used to fill the aural spectrum.

        AT_CALLINGCARD = 2,
        // Links instant message access to the user on the card.
        // : E.G. A card for yourself, for linden support, for
        // : the guy you were talking to in the coliseum.

        AT_LANDMARK = 3,
        // Links to places in the world with location and a screen shot or image saved.
        // : E.G. Home, linden headquarters, the coliseum, destinations where 
        // : we want to increase traffic.

        AT_SCRIPT = 4,
        // Valid scripts that can be attached to an object.
        // : E.G. Open a door, jump into the air.

        AT_CLOTHING = 5,
        // A collection of textures and parameters that can be worn by an avatar.

        AT_OBJECT = 6,
        // Any combination of textures, sounds, and scripts that are
        // associated with a fixed piece of geometry.
        // : E.G. A hot tub, a house with working door.

        AT_NOTECARD = 7,
        // Just text.

        AT_CATEGORY = 8,
        // Holds a collection of inventory items.
        // It's treated as an item in the inventory and therefore needs a type.

        AT_LSL_TEXT = 10,
        AT_LSL_BYTECODE = 11,
        // The LSL is the scripting language. 
        // We've split it into a text and bytecode representation.

        AT_TEXTURE_TGA = 12,
        // Uncompressed TGA texture.

        AT_BODYPART = 13,
        // A collection of textures and parameters that can be worn by an avatar.

        AT_SOUND_WAV = 17,
        // Uncompressed sound.

        AT_IMAGE_TGA = 18,
        // Uncompressed image, non-square.
        // Not appropriate for use as a texture.

        AT_IMAGE_JPEG = 19,
        // Compressed image, non-square.
        // Not appropriate for use as a texture.

        AT_ANIMATION = 20,
        // Animation.

        AT_GESTURE = 21,
        // Gesture, sequence of animations, sounds, chat, wait steps.

        AT_SIMSTATE = 22,
        // Simstate file.

        AT_LINK = 24,
        // Inventory symbolic link

        AT_LINK_FOLDER = 25,
        // Inventory folder link

        AT_MARKETPLACE_FOLDER = 26,
        // Marketplace folder. Same as an AT_CATEGORY but different display methods.

        AT_WIDGET = 40,
        // UI Widget: this is *not* an inventory asset type, only a viewer side asset (e.g. button, other ui items...)

        AT_PERSON = 45,
        // A user uuid  which is not an inventory asset type, used in viewer only for adding a person to a chat via drag and drop.

        AT_MESH = 49,
        // Mesh data in our proprietary SLM format

        AT_COUNT = 50,

        // +*********************************************************+
        // |  TO ADD AN ELEMENT TO THIS ENUM:                        |
        // +*********************************************************+
        // | 1. INSERT BEFORE AT_COUNT                               |
        // | 2. INCREMENT AT_COUNT BY 1                              |
        // | 3. ADD TO LLAssetType.cpp                               |
        // | 4. ADD TO LLViewerAssetType.cpp                         |
        // | 5. ADD TO DEFAULT_ASSET_FOR_INV in LLInventoryType.cpp  |
        // +*********************************************************+
        AT_UNKNOWN = 255,
        AT_NONE = -1
    };

}
