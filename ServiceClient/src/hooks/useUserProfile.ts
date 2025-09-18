// hooks/useUserProfile.ts
import { useState, useEffect, useCallback } from 'react';
import type { UserProfile, UpdateProfileRequest } from '../Models/user';
import { userProfileService } from '../Services/userProfileService';
import { message } from 'antd';

export const useUserProfile = (userId?: string) => {
  const [profile, setProfile] = useState<UserProfile | null>(null);
  const [loading, setLoading] = useState(true);
  const [updating, setUpdating] = useState(false);
  const [uploading, setUploading] = useState(false);

  const fetchProfile = useCallback(async () => {
    try {
      setLoading(true);
      const userProfile = userId 
        ? await userProfileService.getProfileById(userId)
        : await userProfileService.getMyProfile();
      setProfile(userProfile);
    } catch (error: any) {
      message.error('Ошибка загрузки профиля: ' + error.message);
    } finally {
      setLoading(false);
    }
  }, [userId]);

  const updateProfile = async (data: UpdateProfileRequest) => {
    try {
      setUpdating(true);
      const updatedProfile = await userProfileService.updateProfile(data);
      setProfile(updatedProfile);
      message.success('Профиль успешно обновлен');
      return updatedProfile;
    } catch (error: any) {
      message.error('Ошибка обновления профиля: ' + error.message);
      throw error;
    } finally {
      setUpdating(false);
    }
  };

  const uploadAvatar = async (file: File) => {
    try {
      setUploading(true);
      const result = await userProfileService.uploadAvatar(file);
      setProfile(prev => prev ? { ...prev, Avatar: result.Avatar } : null);
      message.success('Аватар успешно обновлен');
    } catch (error: any) {
      message.error('Ошибка загрузки аватара: ' + error.message);
      throw error;
    } finally {
      setUploading(false);
    }
  };

  const resendConfirmationEmail = async () => {
    try {
      await userProfileService.resendConfirmationEmail();
      message.success('Письмо с подтверждением отправлено');
    } catch (error: any) {
      message.error('Ошибка отправки письма: ' + error.message);
    }
  };

  useEffect(() => {
    fetchProfile();
  }, [fetchProfile]);

  return {
    profile,
    loading,
    updating,
    uploading,
    fetchProfile,
    updateProfile,
    uploadAvatar,
    resendConfirmationEmail,
  };
};