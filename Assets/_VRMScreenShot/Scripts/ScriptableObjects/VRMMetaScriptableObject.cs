using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRM;

namespace jp.netsis.VRMScreenShot
{
    [CreateAssetMenu(fileName = "VRMMetaScriptableObject", menuName = "VRMScreenShot/Create VRMMetaScriptableObject")]
    public class VRMMetaScriptableObject : ScriptableObject
    {
        private VRMMetaObject _vrmMetaObject;

        public VRMMetaObject VrmMetaObject
        {
            set => _vrmMetaObject = value;
            get => _vrmMetaObject;
        }

        
        [SerializeField, Header("- Icon Textures ResourceList -")]
        List<Texture2D> _textureAllowedUserResourceList;
        [SerializeField]
        List<Texture2D> _textureViolentUssageResourceList;
        [SerializeField]
        List<Texture2D> _textureSexualUssageResourceList;
        [SerializeField]
        List<Texture2D> _textureCommercialUssageResourceList;

        public List<Texture2D> TextureAllowedUserResourceList => _textureAllowedUserResourceList;
        public List<Texture2D> TextureViolentUssageResourceList => _textureViolentUssageResourceList;
        public List<Texture2D> TextureSexualUssageResourceList => _textureSexualUssageResourceList;
        public List<Texture2D> TextureCommercialUssageResourceList => _textureCommercialUssageResourceList;
    }
}