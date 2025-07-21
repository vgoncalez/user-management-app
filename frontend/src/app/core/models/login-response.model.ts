import { User } from "../../features/users/models/user.model";

export interface LoginResponse {
    user: User
    accessToken: string;
}