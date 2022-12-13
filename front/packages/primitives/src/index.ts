/*
 * Kyoo - A portable and vast media library solution.
 * Copyright (c) Kyoo.
 *
 * See AUTHORS.md and LICENSE file in the project root for full license information.
 *
 * Kyoo is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * any later version.
 *
 * Kyoo is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with Kyoo. If not, see <https://www.gnu.org/licenses/>.
 */

export { Header, Main, Nav, Footer, UL } from "@expo/html-elements";
export * from "./text";
export * from "./themes";
export * from "./icons";
export * from "./links";
export * from "./avatar";
export * from "./image";
export * from "./skeleton";
export * from "./tooltip";
export * from "./container";
export * from "./divider";

export * from "./animated";

export * from "./utils/breakpoints";
export * from "./utils/nojs";
export * from "./utils/head";

import { px } from "yoshiki/native";

export const ts = (spacing: number) => {
	return px(spacing * 8);
};
