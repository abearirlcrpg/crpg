import Platform from './platform';
import Role from './role';

export default interface User {
  id: number;
  platform: Platform;
  platformUserId: number;
  name: string;
  gold: number;
  heirloomPoints: number;
  role: Role;
  avatarSmall: string;
  avatarMedium: string;
  avatarFull: string;
}