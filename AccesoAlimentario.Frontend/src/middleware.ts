import {NextRequest} from "next/server";

const protectedRoutes = ["/admin/inicio"];

export function middleware(req: NextRequest) {
    const sessionCookie = req.cookies.get("session");
    if (!sessionCookie && protectedRoutes.includes(req.nextUrl.pathname)) {
        console.log("Redirecting to login");
        return Response.redirect(new URL("/login", req.nextUrl));
    }
}