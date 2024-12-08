import {NextRequest} from "next/server";

const protectedRoutes = "/admin";

export function middleware(req: NextRequest) {
    const sessionCookie = req.cookies.get("session");
    if (!sessionCookie && (req.nextUrl.pathname.startsWith(protectedRoutes))) {
        return Response.redirect(new URL("/login", req.nextUrl));
    }
    if (sessionCookie && (req.nextUrl.pathname === "/login")) {
        return Response.redirect(new URL("/admin/inicio", req.nextUrl));
    }
}