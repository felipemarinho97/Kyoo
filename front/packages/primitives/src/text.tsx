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

import { ComponentType, ComponentProps } from "react";
import { Platform, Text, TextProps, TextStyle } from "react-native";
import { percent, px, rem, useYoshiki } from "yoshiki/native";
import {
	H1 as EH1,
	H2 as EH2,
	H3 as EH3,
	H4 as EH4,
	H5 as EH5,
	H6 as EH6,
	P as EP,
	LI as ELI,
} from "@expo/html-elements";
import { ts } from ".";

const styleText = (
	Component: ComponentType<ComponentProps<typeof EP>>,
	type?: "header" | "sub",
	custom?: TextStyle,
) => {
	const Text = (props: ComponentProps<typeof EP>) => {
		const { css, theme } = useYoshiki();

		return (
			<Component
				{...css(
					[
						{
							marginTop: 0,
							marginBottom: rem(0.5),
							// fontFamily: type === "header" ? theme.fonts.heading : theme.fonts.paragraph,
							color: type === "header" ? theme.heading : theme.paragraph,
						},
						type === "sub" && { fontWeight: "300", opacity: 0.8, fontSize: rem(0.8) },
						custom,
					],
					props as TextProps,
				)}
			/>
		);
	};
	return Text;
};

export const H1 = styleText(EH1, "header", { fontSize: rem(3) });
export const H2 = styleText(EH2, "header", { fontSize: rem(2) });
export const H3 = styleText(EH3, "header");
export const H4 = styleText(EH4, "header");
export const H5 = styleText(EH5, "header");
export const H6 = styleText(EH6, "header");
export const Heading = styleText(EP, "header");
export const P = styleText(EP, undefined, { fontSize: rem(1) });
export const SubP = styleText(EP, "sub");

export const LI = ({ children, ...props }: TextProps) => {
	const { css } = useYoshiki();

	return (
		<P accessibilityRole="listitem" {...props}>
			<Text
				{...css({
					height: percent(100),
					marginBottom: rem(0.5),
					paddingRight: ts(1),
				})}
			>
				{String.fromCharCode(0x2022)}
			</Text>
			{children}
		</P>
	);
};
