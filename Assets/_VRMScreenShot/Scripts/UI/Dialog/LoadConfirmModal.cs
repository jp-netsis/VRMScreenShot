using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRM;

namespace jp.netsis.VRMScreenShot
{
    public class LoadConfirmModal : MonoBehaviour
    {
        [SerializeField]
        private VRMMetaScriptableObject _vrmMetaScriptableObject;

        [Header("- Info -")]
        [SerializeField]
        RawImage _thumbnail;

        [Header("- CharacterPermission -")]
        [SerializeField]
        private TMP_Text _headline;
        [SerializeField]
        private TMP_Text _previewTitle;
        [SerializeField]
        private TMP_Text _previewVersion;
        [SerializeField]
        private TMP_Text _previewAuthor;
        [SerializeField]
        private TMP_Text _previewContact;
        [SerializeField]
        private TMP_Text _previewReference;
        [SerializeField]
        private TMP_Text _previewPermissionAct;
        [SerializeField]
        private TMP_Text _previewPermissionVioient;
        [SerializeField]
        private TMP_Text _previewPermissionSexual;
        [SerializeField]
        private TMP_Text _previewPermissionCommercial;
        [SerializeField]
        private TMP_Text _previewPermissionOther;
        [Header("- CharacterPermission Images -")]
        [SerializeField]
        RawImage _previewPermissionActIcon;
        [SerializeField]
        RawImage _previewPermissionVioientIcon;
        [SerializeField]
        RawImage _previewPermissionSexualIcon;
        [SerializeField]
        RawImage _previewPermissionCommercialIcon;

        [Header("- DistributionLicense -")]
        [SerializeField]
        private TMP_Text _previewDistributionLicense;
        [SerializeField]
        private TMP_Text _previewPDistributionOther;

        [Header("- CopySuccessText -")]
        [SerializeField]
        private TMP_Text _permissionOtherCopySuccessText;
        [SerializeField]
        private TMP_Text _distributionOtherCopySuccessText;
        private void OnEnable()
        {
            if (null != _vrmMetaScriptableObject)
            {
                SetMeta(_vrmMetaScriptableObject.VrmMetaObject);
            }
        }

        public void SetMeta(VRMMetaObject meta)
        {
            _previewTitle.text = meta.Title;
            _previewVersion.text = meta.Version;
            _previewAuthor.text = meta.Author;
            _previewContact.text = meta.ContactInformation;
            _previewReference.text = meta.Reference;

            string value = string.Empty;
            LocalizationSelectorScriptableObject.Instance.TryGetValue($"PermissionAct{(int)meta.AllowedUser}", out value);
            _previewPermissionAct.text = value;
            LocalizationSelectorScriptableObject.Instance.TryGetValue($"PermissionUsage{(int)meta.ViolentUssage}", out value);
            _previewPermissionVioient.text = value;
            LocalizationSelectorScriptableObject.Instance.TryGetValue($"PermissionUsage{(int)meta.SexualUssage}", out value);
            _previewPermissionSexual.text = value;
            LocalizationSelectorScriptableObject.Instance.TryGetValue($"PermissionUsage{(int)meta.CommercialUssage}", out value);
            _previewPermissionCommercial.text = value;
            _previewPermissionOther.text = meta.OtherPermissionUrl;
                
            // DistributionLicense
            LocalizationSelectorScriptableObject.Instance.TryGetValue($"VRMLicenseType{(int)meta.LicenseType}", out value);
            _previewDistributionLicense.text = value;
            _previewPDistributionOther.text = meta.OtherLicenseUrl;

            // CharacterPermission Images
            _previewPermissionActIcon.texture = _vrmMetaScriptableObject.TextureAllowedUserResourceList[(int)meta.AllowedUser];
            _previewPermissionVioientIcon.texture = _vrmMetaScriptableObject.TextureViolentUssageResourceList[(int)meta.ViolentUssage];
            _previewPermissionSexualIcon.texture = _vrmMetaScriptableObject.TextureSexualUssageResourceList[(int)meta.SexualUssage];
            _previewPermissionCommercialIcon.texture = _vrmMetaScriptableObject.TextureCommercialUssageResourceList[(int)meta.CommercialUssage];
            // Thumbnail
            if (meta.Thumbnail)
            {
                _thumbnail.texture = meta.Thumbnail;
            }
        }

        public void OnPermissionOtherCopyClick()
        {
            GUIUtility.systemCopyBuffer = _previewPDistributionOther.text;
            StartCoroutine(PermissionOtherCopySuccessAnimation());
        }
        IEnumerator PermissionOtherCopySuccessAnimation()
        {
            yield return CopyAnimation(_permissionOtherCopySuccessText);
        }

        public void OnDistributionOtherCopyClick()
        {
            GUIUtility.systemCopyBuffer = _previewPDistributionOther.text;
            StartCoroutine(DistributionOtherCopySuccessAnimation());
        }
        IEnumerator DistributionOtherCopySuccessAnimation()
        {
            yield return CopyAnimation(_distributionOtherCopySuccessText);
        }

        IEnumerator CopyAnimation(TMP_Text text)
        {
            text.gameObject.SetActive(true);
            yield return new WaitForSeconds(1);
            text.gameObject.SetActive(false);
        }
    }
}