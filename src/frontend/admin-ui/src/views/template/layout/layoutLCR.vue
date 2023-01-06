<template>
	<el-container>
		<el-aside>
			<el-container>
				<el-header>Left Header</el-header>
				<el-main>Left Main</el-main>
				<el-footer>Left Footer</el-footer>
			</el-container>
		</el-aside>
		<el-container>
			<el-header>Header</el-header>
			<el-main class="nopadding">
				<el-result icon="info" title="Tip" sub-title="可根据 <el-container> <el-header> <el-aside> <el-main> <el-footer> 灵活运用达到各种布局"></el-result>
			</el-main>
			<el-footer>Footer</el-footer>
		</el-container>
		<el-aside>
			<el-container>
				<el-header>Right Header</el-header>
				<el-main>Right Main</el-main>
				<el-footer>Right Footer</el-footer>
			</el-container>
		</el-aside>
	</el-container>
</template>

<script>
	export default {
		name: 'layoutLCR',
		data() {
			return {

			}
		}
	}
</script>

<style>
</style>